using System.Collections.Concurrent;
using System.Collections.Immutable;
using Api.Core.Domain.Common;
using Api.Sync.Core.Application.Api.Commands.ProcessApiRequest;
using Api.Sync.Core.Application.Api.Queries.GetPendingApiRequests;
using Api.Sync.Core.Application.Common.Models;
using Api.Sync.Core.Application.ContpaqiComercial.Commands.AbrirEmpresa;
using Api.Sync.Core.Application.ContpaqiComercial.Interfaces;
using ARSoftware.Contpaqi.Api.Common.Domain;
using ARSoftware.Contpaqi.Comercial.Sdk.Abstractions.Models;
using MediatR;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Options;

namespace Api.Sync.Presentation.WorkerService;

public sealed class Worker : BackgroundService
{
    private readonly ApiSyncConfig _apiSyncConfig;
    private readonly HubConnection _connection;
    private readonly ContpaqiComercialConfig _contpaqiComercialConfig;
    private readonly IEmpresaRepository _empresaRepository;
    private readonly IHostApplicationLifetime _hostApplicationLifetime;
    private readonly ILogger<Worker> _logger;
    private readonly IMediator _mediator;
    private readonly ConcurrentQueue<GetPendingRequestsMessage> _pendingRequestQueue = new();

    public Worker(ILogger<Worker> logger, IMediator mediator, IHostApplicationLifetime hostApplicationLifetime,
        IOptions<ApiSyncConfig> apiSyncConfigOptions, IOptions<ContpaqiComercialConfig> contpaqiComercialConfigOptions,
        IEmpresaRepository empresaRepository, ApiRequestHubClientFactory apiRequestHubClientFactory)
    {
        _logger = logger;
        _mediator = mediator;
        _hostApplicationLifetime = hostApplicationLifetime;
        _empresaRepository = empresaRepository;
        _apiSyncConfig = apiSyncConfigOptions.Value;
        _contpaqiComercialConfig = contpaqiComercialConfigOptions.Value;

        _connection = apiRequestHubClientFactory.BuildConnection();
        _connection.On<GetPendingRequestsMessage>("GetPendingRequests", getPendingRequestMessage =>
        {
            _logger.LogInformation("Get pending requests notification received: {GetPendingRequestsMessage}", getPendingRequestMessage);
            if (_apiSyncConfig.Empresas.Contains(getPendingRequestMessage.EmpresaRfc))
                _pendingRequestQueue.Enqueue(getPendingRequestMessage);
        });

        foreach (string empresa in _apiSyncConfig.Empresas)
            _pendingRequestQueue.Enqueue(new GetPendingRequestsMessage(_apiSyncConfig.SubscriptionKey, empresa));
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await DoWork(stoppingToken);
    }

    private async Task DoWork(CancellationToken stoppingToken)
    {
        try
        {
            _logger.LogInformation("Iniciando conexión con SignalR.");
            await _connection.StartAsync(stoppingToken);

            ImmutableList<Empresa> empresas = (await _empresaRepository.BuscarTodoAsync(LoadRelatedDataOptions.Default, stoppingToken))
                .ToImmutableList();

            while (!stoppingToken.IsCancellationRequested)
            {
                if (_pendingRequestQueue.IsEmpty)
                {
                    _logger.LogDebug("Esperando solicitudes nuevas.");
                    await Task.Delay(1000, stoppingToken);
                    continue;
                }

                _logger.LogInformation("Procesando solicitudes pendientes.");

                if (_pendingRequestQueue.TryDequeue(out GetPendingRequestsMessage? getPendingRequestMessage))
                {
                    Empresa? empresa = empresas.FirstOrDefault(e => e.Parametros!.Rfc == getPendingRequestMessage.EmpresaRfc);

                    if (empresa is null) continue;

                    _contpaqiComercialConfig.Empresa = empresa;

                    List<ApiRequest> apiRequests = (await _mediator.Send(new GetPendingRequestsQuery(), stoppingToken)).ToList();
                    _logger.LogInformation("{PendingRequests} solicitudes pendientes.", apiRequests.Count);

                    if (!apiRequests.Any()) continue;

                    await _mediator.Send(new AbrirEmpresaCommand(), stoppingToken);

                    foreach (ApiRequest apiRequest in apiRequests)
                    {
                        int requestIndex = apiRequests.IndexOf(apiRequest) + 1;
                        int requestsCount = apiRequests.Count;
                        _logger.LogInformation("Empresa: {EmpresaRfc}. Procesando [{requestIndex} of {requestsCount}]",
                            empresa.Parametros!.Rfc, requestIndex, requestsCount);

                        await _mediator.Send(new ProcessApiRequestCommand(apiRequest), stoppingToken);
                    }
                }

                if (_apiSyncConfig.ShouldShutDown())
                {
                    _logger.LogInformation("La aplicación debe apagarse.");
                    break;
                }
            }
        }
        catch (OperationCanceledException e)
        {
            _logger.LogWarning(e, "Operation was cancelled.");
        }
        catch (Exception e)
        {
            _logger.LogCritical(e, "Critical error occurred.");
        }

        _hostApplicationLifetime.StopApplication();
    }

    public override async Task StopAsync(CancellationToken cancellationToken)
    {
        await _connection.DisposeAsync();
    }
}
