using System.Diagnostics;
using Api.Sync.Core.Application.Api.Commands.SendApiResponse;
using ARSoftware.Contpaqi.Api.Common.Domain;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Api.Sync.Core.Application.Api.Commands.ProcessApiRequest;

public sealed record ProcessApiRequestCommand(ApiRequest ApiRequest) : IRequest;

public sealed class ProcessApiRequestCommandHandler : IRequestHandler<ProcessApiRequestCommand>
{
    private readonly ILogger<ProcessApiRequestCommandHandler> _logger;
    private readonly IMediator _mediator;
    private readonly Stopwatch _timer;

    public ProcessApiRequestCommandHandler(IMediator mediator, ILogger<ProcessApiRequestCommandHandler> logger)
    {
        _timer = new Stopwatch();

        _mediator = mediator;
        _logger = logger;
    }

    public async Task Handle(ProcessApiRequestCommand request, CancellationToken cancellationToken)
    {
        try
        {
            _timer.Start();
            var contpaqiResposne = await _mediator.Send(request.ApiRequest.ContpaqiRequest, cancellationToken) as ContpaqiResponse;
            _timer.Stop();

            var apiResponse = ApiResponse.CreateSuccessfull(contpaqiResposne ?? throw new InvalidOperationException());
            apiResponse.ExecutionTime = _timer.ElapsedMilliseconds;

            await _mediator.Send(new SendApiResponseCommand(request.ApiRequest.Id, apiResponse), cancellationToken);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error Processing {RequestName} {ApiRequestId} {@ContpaqiRequest}",
                nameof(request.ApiRequest.ContpaqiRequest), request.ApiRequest.Id, request.ApiRequest.ContpaqiRequest);

            await _mediator.Send(new SendApiResponseCommand(request.ApiRequest.Id, ApiResponse.CreateFailed(e)), cancellationToken);
        }
    }
}
