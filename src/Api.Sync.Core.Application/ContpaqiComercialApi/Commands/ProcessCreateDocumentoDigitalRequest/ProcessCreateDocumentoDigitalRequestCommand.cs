using Api.Core.Domain.Models;
using Api.Core.Domain.Requests;
using Api.Sync.Core.Application.Documentos.Commands.GenerarDocumentosDigitales;
using ARSoftware.Contpaqi.Comercial.Sdk.Extras.Models.Enums;
using MediatR;

namespace Api.Sync.Core.Application.ContpaqiComercialApi.Commands.ProcessCreateDocumentoDigitalRequest;

public sealed record ProcessCreateDocumentoDigitalRequestCommand
    (CreateDocumentoDigitalRequest ApiRequest) : IRequest<CreateDocumentoDigitalResponse>;

public sealed class
    ProcessEmitDocumentoRequestCommandHandler : IRequestHandler<ProcessCreateDocumentoDigitalRequestCommand, CreateDocumentoDigitalResponse>
{
    private readonly IMediator _mediator;

    public ProcessEmitDocumentoRequestCommandHandler(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task<CreateDocumentoDigitalResponse> Handle(ProcessCreateDocumentoDigitalRequestCommand request,
                                                             CancellationToken cancellationToken)
    {
        CreateDocumentoDigitalRequest apiRequest = request.ApiRequest;

        string rutaDocumento = await _mediator.Send(new GenerarDocumentosDigitalesCommand(apiRequest.Model, apiRequest.Options),
            cancellationToken);

        return CreateDocumentoDigitalResponse.CreateSuccessfull(apiRequest,
            new DocumentoDigital
            {
                Ubicacion = rutaDocumento,
                Nombre = new FileInfo(rutaDocumento).Name,
                Tipo = apiRequest.Options.Tipo == TipoArchivoDigital.Pdf ? "application/pdf" : "text/xml"
            });
    }
}
