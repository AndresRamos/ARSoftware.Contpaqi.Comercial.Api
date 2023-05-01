using System.Collections.Immutable;
using Api.Core.Domain.Models;
using Api.Sync.Core.Application.Api.Commands.ProcessApiRequest;
using Api.Sync.Core.Application.Api.Queries.GetPendingApiRequests;
using Api.Sync.Core.Application.Common.Models;
using Api.Sync.Core.Application.ContpaqiComercial.Commands.AbrirEmpresa;
using Api.Sync.Core.Application.ContpaqiComercial.Interfaces;
using ARSoftware.Contpaqi.Api.Common.Domain;
using MediatR;
using Microsoft.Extensions.Options;

namespace Api.Sync.Presentation.WorkerService;

public sealed class Worker : BackgroundService
{
    private readonly ApiSyncConfig _apiSyncConfig;
    private readonly ContpaqiComercialConfig _contpaqiComercialConfig;
    private readonly IEmpresaRepository _empresaRepository;
    private readonly IHostApplicationLifetime _hostApplicationLifetime;
    private readonly ILogger<Worker> _logger;
    private readonly IMediator _mediator;

    public Worker(ILogger<Worker> logger, IMediator mediator, IHostApplicationLifetime hostApplicationLifetime,
        IOptions<ApiSyncConfig> apiSyncConfigOptions, IOptions<ContpaqiComercialConfig> contpaqiComercialConfigOptions,
        IEmpresaRepository empresaRepository)
    {
        _logger = logger;
        _mediator = mediator;
        _hostApplicationLifetime = hostApplicationLifetime;
        _empresaRepository = empresaRepository;
        _apiSyncConfig = apiSyncConfigOptions.Value;
        _contpaqiComercialConfig = contpaqiComercialConfigOptions.Value;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await DoWork(stoppingToken);
    }

    private async Task DoWork(CancellationToken stoppingToken)
    {
        try
        {
            ImmutableList<Empresa> empresas = (await _empresaRepository.BuscarTodoAsync(LoadRelatedDataOptions.Default, stoppingToken))
                .ToImmutableList();

            while (!stoppingToken.IsCancellationRequested)
            {
                Task waitingTask = Task.Delay(_apiSyncConfig.WaitTime.ToTimeSpan(), stoppingToken);

                foreach (string empresaRfc in _apiSyncConfig.Empresas)
                {
                    Empresa? empresa = empresas.FirstOrDefault(e => e.Rfc == empresaRfc);

                    if (empresa is null)
                        continue;

                    _contpaqiComercialConfig.Empresa = empresa;

                    List<ApiRequest> apiRequests = (await _mediator.Send(new GetPendingRequestsQuery(), stoppingToken)).ToList();
                    _logger.LogInformation("{PendingRequests} solicitudes pendientes.", apiRequests.Count);

                    if (!apiRequests.Any())
                        continue;

                    await _mediator.Send(new AbrirEmpresaCommand(), stoppingToken);

                    foreach (ApiRequest apiRequest in apiRequests)
                    {
                        int requestIndex = apiRequests.IndexOf(apiRequest) + 1;
                        int requestsCount = apiRequests.Count;
                        _logger.LogInformation("Empresa: {EmpresaRfc}. Procesando [{requestIndex} of {requestsCount}]", empresaRfc,
                            requestIndex, requestsCount);

                        await _mediator.Send(new ProcessApiRequestCommand(apiRequest), stoppingToken);
                    }
                }

                if (_apiSyncConfig.ShouldShutDown())
                {
                    _logger.LogInformation("La aplicacion debe apgarse.");
                    break;
                }

                if (_apiSyncConfig.WaitTime != TimeOnly.MinValue)
                {
                    _logger.LogDebug("Esperando la siguiente iteracion.");
                    await waitingTask;
                }
            }
        }
        catch (OperationCanceledException e)
        {
            _logger.LogWarning(e, "Operation was cancelled.");
        }
        catch (Exception e)
        {
            _logger.LogCritical(e, "Critical error ocurred.");
        }

        _hostApplicationLifetime.StopApplication();
    }
}
