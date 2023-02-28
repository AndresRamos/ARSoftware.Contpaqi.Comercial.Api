using MediatR.Pipeline;
using Microsoft.Extensions.Logging;

namespace Api.Sync.Core.Application.Common.Behaviours;

public sealed class LoggingBehaviour<TRequest> : IRequestPreProcessor<TRequest> where TRequest : notnull
{
    private readonly ILogger _logger;

    public LoggingBehaviour(ILogger<TRequest> logger)
    {
        _logger = logger;
    }

    public Task Process(TRequest request, CancellationToken cancellationToken)
    {
        string requestName = typeof(TRequest).Name;

        _logger.LogInformation("API Sync Request: {Name} {@Request}", requestName, request);

        return Task.CompletedTask;
    }
}
