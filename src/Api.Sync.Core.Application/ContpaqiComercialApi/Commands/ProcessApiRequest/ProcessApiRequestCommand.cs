using Api.Core.Domain.Common;
using Api.Sync.Core.Application.ContpaqiComercialApi.Commands.SendApiResponse;
using MediatR;

namespace Api.Sync.Core.Application.ContpaqiComercialApi.Commands.ProcessApiRequest;

public sealed record ProcessApiRequestCommand(ApiRequestBase ApiRequest) : IRequest;

public sealed class ProcessApiRequestCommandHandler : IRequestHandler<ProcessApiRequestCommand>
{
    private readonly IMediator _mediator;

    public ProcessApiRequestCommandHandler(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task Handle(ProcessApiRequestCommand request, CancellationToken cancellationToken)
    {
        ApiRequestBase apiRequest = request.ApiRequest;

        ApiResponseBase apiResponse = await _mediator.Send(apiRequest, cancellationToken);

        await _mediator.Send(new SendApiResponseCommand(apiResponse), cancellationToken);
    }
}
