using Api.Core.Domain.Common;
using Api.Sync.Core.Application.Common.Models;
using Api.Sync.Core.Application.ContpaqiComercial.Commands.AbrirEmpresa;
using Api.Sync.Core.Application.ContpaqiComercial.Queries.BuscarEmpresaPorRfc;
using Api.Sync.Core.Application.ContpaqiComercialApi.Commands.ProcessApiRequest;
using Api.Sync.Core.Application.ContpaqiComercialApi.Queries.GetPendingApiRequests;
using MediatR;
using Microsoft.Extensions.Options;

namespace Api.Sync.Presentation.WorkerService;

public sealed class Worker : BackgroundService
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
        await DoWork(stoppingToken);
    }

    private async Task DoWork(CancellationToken stoppingToken)
    {
        try
        {
            _contpaqiComercialConfig.Empresa = await _mediator.Send(new BuscarEmpresaPorRfcQuery(_apiSyncConfig.EmpresaRfc), stoppingToken);

            await _mediator.Send(new AbrirEmpresaCommand(), stoppingToken);

            while (!stoppingToken.IsCancellationRequested)
            {
                List<ApiRequestBase> apiRequests = (await _mediator.Send(new GetPendingApiRequestsQuery(), stoppingToken)).ToList();
                _logger.LogInformation("{PendingRequests} pending requests.", apiRequests.Count);

                foreach (ApiRequestBase apiRequest in apiRequests)
                {
                    int requestIndex = apiRequests.IndexOf(apiRequest) + 1;
                    int requestsCount = apiRequests.Count;
                    _logger.LogInformation("Processing request [{requestIndex} of {requestsCount}]", requestIndex, requestsCount);

                    await _mediator.Send(new ProcessApiRequestCommand(apiRequest), stoppingToken);
                }

                if (_apiSyncConfig.ShouldShutDown())
                {
                    _logger.LogInformation("Application should shut down.");
                    break;
                }

                if (_apiSyncConfig.WaitTime != TimeOnly.MinValue)
                {
                    var timeSpan = _apiSyncConfig.WaitTime.ToTimeSpan();
                    _logger.LogInformation("Waiting {TimeSpan} for next run.", timeSpan);
                    await Task.Delay(timeSpan, stoppingToken);
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

    //private static async Task<bool> WaitForAppStartup(IHostApplicationLifetime lifetime, CancellationToken stoppingToken)
    //{
    //    var startedSource = new TaskCompletionSource();
    //    var cancelledSource = new TaskCompletionSource();

    //    using CancellationTokenRegistration reg1 = lifetime.ApplicationStarted.Register(() => startedSource.SetResult());
    //    using CancellationTokenRegistration reg2 = stoppingToken.Register(() => cancelledSource.SetResult());

    //    Task completedTask = await Task.WhenAny(startedSource.Task, cancelledSource.Task).ConfigureAwait(false);

    //    // If the completed tasks was the "app started" task, return true, otherwise false
    //    return completedTask == startedSource.Task;
    //}
}
