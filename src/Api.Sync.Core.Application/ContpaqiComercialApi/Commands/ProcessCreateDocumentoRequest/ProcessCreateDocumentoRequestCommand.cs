using Api.Core.Domain.Requests;
using Api.Sync.Core.Application.ContpaqiComercial.Interfaces;
using Api.Sync.Core.Application.Documentos.Commands.CreateDocumento;
using Api.Sync.Core.Application.Documentos.Commands.TimbrarDocumento;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Api.Sync.Core.Application.ContpaqiComercialApi.Commands.ProcessCreateDocumentoRequest;

public sealed record ProcessCreateDocumentoRequestCommand(CreateDocumentoRequest ApiRequest) : IRequest<CreateDocumentoResponse>;

public sealed class
    ProcessCreateDocumentoRequestCommandHandler : IRequestHandler<ProcessCreateDocumentoRequestCommand, CreateDocumentoResponse>
{
    private readonly IDocumentoRepository _documentoRepository;
    private readonly ILogger _logger;
    private readonly IMediator _mediator;

    public ProcessCreateDocumentoRequestCommandHandler(IDocumentoRepository documentoRepository,
                                                       IMediator mediator,
                                                       ILogger<ProcessCreateDocumentoRequestCommandHandler> logger)
    {
        _documentoRepository = documentoRepository;
        _mediator = mediator;
        _logger = logger;
    }

    public async Task<CreateDocumentoResponse> Handle(ProcessCreateDocumentoRequestCommand request, CancellationToken cancellationToken)
    {
        CreateDocumentoRequest apiRequest = request.ApiRequest;

        var documentoId = 0;

        try
        {
            documentoId = await _mediator.Send(new CreateDocumentoCommand(apiRequest.Model, apiRequest.Options), cancellationToken);

            if (apiRequest.Options.TimbrarDocumento)
                await _mediator.Send(new TimbrarDocumentoCommand(documentoId, apiRequest.Options.ContrasenaCertificado), cancellationToken);
        }
        catch (Exception e)
        {
            return CreateDocumentoResponse.CreateFailed(apiRequest, e.Message);
        }

        return CreateDocumentoResponse.CreateSuccessfull(apiRequest,
            await _documentoRepository.BuscarDocumentoPorIdAsync(documentoId, cancellationToken));
    }
}
