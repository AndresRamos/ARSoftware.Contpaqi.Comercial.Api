using Api.Core.Domain.Common;
using Api.Sync.Core.Application.Common.Models;
using Api.Sync.Core.Application.ContpaqiComercial.Commands.AbrirEmpresa;
using Api.Sync.Core.Application.ContpaqiComercial.Commands.CerrarEmpresa;
using Api.Sync.Core.Application.ContpaqiComercial.Commands.IniciarSdk;
using Api.Sync.Core.Application.ContpaqiComercial.Commands.TerminarSdk;
using Api.Sync.Core.Application.ContpaqiComercial.Queries.BuscarEmpresaPorRfc;
using Api.Sync.Core.Application.ContpaqiComercialApi.Commands.ProcessApiRequest;
using Api.Sync.Core.Application.ContpaqiComercialApi.Queries.GetPendingApiRequests;
using MediatR;
using Microsoft.Extensions.Options;

namespace Api.Sync.Presentation.WorkerService;

public class Worker : BackgroundService
{
    private readonly ApiSyncConfig _apiSyncConfig;
    private readonly ContpaqiComercialConfig _contpaqiComercialConfig;
    private readonly IHostApplicationLifetime _hostApplicationLifetime;
    private readonly ILogger<Worker> _logger;
    private readonly IMediator _mediator;

    public Worker(ILogger<Worker> logger,
                  IMediator mediator,
                  IHostApplicationLifetime hostApplicationLifetime,
                  IOptions<ApiSyncConfig> apiSyncConfigOptions,
                  IOptions<ContpaqiComercialConfig> contpaqiComercialConfigOptions)
    {
        _logger = logger;
        _mediator = mediator;
        _hostApplicationLifetime = hostApplicationLifetime;
        _apiSyncConfig = apiSyncConfigOptions.Value;
        _contpaqiComercialConfig = contpaqiComercialConfigOptions.Value;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        try
        {
            await _mediator.Send(new IniciarSdkCommand());

            _contpaqiComercialConfig.Empresa = await _mediator.Send(new BuscarEmpresaPorRfcQuery(_apiSyncConfig.EmpresaRfc), stoppingToken);

            await _mediator.Send(new AbrirEmpresaCommand());

            while (!stoppingToken.IsCancellationRequested)
            {
                List<ApiRequestBase> apiRequests = (await _mediator.Send(new GetPendingApiRequestsQuery(), stoppingToken)).ToList();

                foreach (ApiRequestBase apiRequest in apiRequests)
                {
                    int requestIndex = apiRequests.IndexOf(apiRequest) + 1;
                    int requestsCount = apiRequests.Count;
                    _logger.LogInformation("Processing request [{requestIndex} of {requestsCount}]", requestIndex, requestsCount);

                    await _mediator.Send(new ProcessApiRequestCommand(apiRequest), stoppingToken);
                }

                //if (TimeOnly.FromDateTime(DateTime.Now) >= _apiSyncConfig.ShutdownTime)
                //    _hostApplicationLifetime.StopApplication();

                var timeSpan = _apiSyncConfig.WaitTime.ToTimeSpan();
                _logger.LogInformation("Waiting {TimeSpan} for next run.", timeSpan);
                await Task.Delay(timeSpan, stoppingToken);
            }
        }
        catch (Exception e)
        {
            _logger.LogCritical(e, "Critical error ocurred.");
        }
        finally
        {
            await _mediator.Send(new CerrarEmpresaCommand());
            await _mediator.Send(new TerminarSdkCommand());
        }
    }
}
