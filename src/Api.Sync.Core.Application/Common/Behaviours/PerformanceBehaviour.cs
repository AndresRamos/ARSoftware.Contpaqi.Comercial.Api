using System.Diagnostics;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Api.Sync.Core.Application.Common.Behaviours;

public class PerformanceBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : notnull
{
    private readonly ILogger<TRequest> _logger;
    private readonly Stopwatch _timer;

    public PerformanceBehaviour(ILogger<TRequest> logger)
    {
        _timer = new Stopwatch();

        _logger = logger;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        _timer.Start();

        TResponse response = await next();

        _timer.Stop();

        long elapsedMilliseconds = _timer.ElapsedMilliseconds;

        string requestName = typeof(TRequest).Name;

        _logger.LogDebug("Request: {Name} ({ElapsedMilliseconds} milliseconds) ", requestName, elapsedMilliseconds);

        return response;
    }
}
