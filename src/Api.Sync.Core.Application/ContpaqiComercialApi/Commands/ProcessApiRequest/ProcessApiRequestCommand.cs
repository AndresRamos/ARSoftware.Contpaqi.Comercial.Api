using Api.Core.Domain.Common;
using Api.Core.Domain.Requests;
using Api.Sync.Core.Application.ContpaqiComercialApi.Commands.ProcessCreateClienteRequest;
using Api.Sync.Core.Application.ContpaqiComercialApi.Commands.ProcessCreateDocumentoDigitalRequest;
using Api.Sync.Core.Application.ContpaqiComercialApi.Commands.ProcessCreateDocumentoRequest;
using Api.Sync.Core.Application.ContpaqiComercialApi.Commands.SendApiResponse;
using MediatR;

namespace Api.Sync.Core.Application.ContpaqiComercialApi.Commands.ProcessApiRequest;

public sealed record ProcessApiRequestCommand(ApiRequestBase ApiRequest) : IRequest
{
}

public sealed class ProcessApiRequestCommandHandler : IRequestHandler<ProcessApiRequestCommand>
{
    private readonly IMediator _mediator;

    public ProcessApiRequestCommandHandler(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task<Unit> Handle(ProcessApiRequestCommand request, CancellationToken cancellationToken)
    {
        ApiRequestBase apiRequest = request.ApiRequest;

        ApiResponseBase apiResponse = apiRequest switch
        {
            CreateDocumentoRequest r => await _mediator.Send(new ProcessCreateDocumentoRequestCommand(r), cancellationToken),
            CreateClienteRequest r => await _mediator.Send(new ProcessCreateClienteRequestCommand(r), cancellationToken),
            CreateDocumentoDigitalRequest r => await _mediator.Send(new ProcessCreateDocumentoDigitalRequestCommand(r), cancellationToken),
            _ => throw new InvalidOperationException("Request is not valid API request.")
        };

        await _mediator.Send(new SendApiResponseCommand(apiResponse), cancellationToken);

        return Unit.Value;
    }
}
