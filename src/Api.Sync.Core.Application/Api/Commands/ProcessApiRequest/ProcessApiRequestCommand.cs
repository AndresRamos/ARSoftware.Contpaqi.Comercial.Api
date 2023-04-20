using Api.Core.Domain.Common;
using Api.Sync.Core.Application.Api.Commands.SendApiResponse;
using MediatR;

namespace Api.Sync.Core.Application.Api.Commands.ProcessApiRequest;

public sealed record ProcessApiRequestCommand(ApiRequest ApiRequest) : IRequest;

public sealed class ProcessApiRequestCommandHandler : IRequestHandler<ProcessApiRequestCommand>
{
    private readonly IMediator _mediator;

    public ProcessApiRequestCommandHandler(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task Handle(ProcessApiRequestCommand request, CancellationToken cancellationToken)
    {
        ApiResponse apiResponse = await _mediator.Send(request.ApiRequest.ContpaqiRequest, cancellationToken);

        await _mediator.Send(new SendApiResponseCommand(request.ApiRequest.Id, apiResponse), cancellationToken);
    }
}
