using MediatR.Pipeline;
using Microsoft.Extensions.Logging;

namespace Api.Sync.Core.Application.Common.Behaviours;

public class LoggingBehaviour<TRequest> : IRequestPreProcessor<TRequest> where TRequest : notnull
{
    private readonly ILogger _logger;

    public LoggingBehaviour(ILogger<TRequest> logger)
    {
        _logger = logger;
    }

    public Task Process(TRequest request, CancellationToken cancellationToken)
    {
        string requestName = typeof(TRequest).Name;

        _logger.LogDebug("Start Thread Id: {ThreadId}", Thread.CurrentThread.ManagedThreadId);
        _logger.LogInformation("API Sync Request: {Name} {@Request}", requestName, request);

        return Task.CompletedTask;
    }
}

public class LoggingBehaviour2<TRequest, TResponse> : IRequestPostProcessor<TRequest, TResponse> where TRequest : notnull
{
    private readonly ILogger _logger;

    public LoggingBehaviour2(ILogger<TRequest> logger)
    {
        _logger = logger;
    }

    public Task Process(TRequest request, TResponse response, CancellationToken cancellationToken)
    {
        _logger.LogDebug("End Thread Id: {ThreadId}", Thread.CurrentThread.ManagedThreadId);

        return Task.CompletedTask;
    }
}
