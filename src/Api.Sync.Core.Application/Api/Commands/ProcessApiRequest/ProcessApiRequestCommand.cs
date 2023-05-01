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

    public ProcessApiRequestCommandHandler(IMediator mediator, ILogger<ProcessApiRequestCommandHandler> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    public async Task Handle(ProcessApiRequestCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var contpaqiResposne = await _mediator.Send(request.ApiRequest.ContpaqiRequest, cancellationToken) as ContpaqiResponse;

            if (contpaqiResposne is null)
                throw new InvalidOperationException("Result is not a ContpaqiResponse.");

            await _mediator.Send(new SendApiResponseCommand(request.ApiRequest.Id, ApiResponse.CreateSuccessfull(contpaqiResposne)),
                cancellationToken);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error Processing {ContpaqiRequest}", nameof(request.ApiRequest.ContpaqiRequest));
            await _mediator.Send(new SendApiResponseCommand(request.ApiRequest.Id, ApiResponse.CreateFailed(e)), cancellationToken);
        }
    }
}
