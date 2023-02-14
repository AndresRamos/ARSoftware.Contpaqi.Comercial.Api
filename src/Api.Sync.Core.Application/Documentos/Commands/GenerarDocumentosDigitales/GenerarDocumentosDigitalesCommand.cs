using Api.Core.Domain.Models;
using Api.Core.Domain.Requests;
using Api.Sync.Core.Application.Common.Models;
using Api.Sync.Core.Application.ContpaqiComercial.Interfaces;
using ARSoftware.Contpaqi.Comercial.Sdk.Extras.Helpers;
using ARSoftware.Contpaqi.Comercial.Sdk.Extras.Interfaces;
using MediatR;
using Microsoft.Extensions.Options;

namespace Api.Sync.Core.Application.Documentos.Commands.GenerarDocumentosDigitales;

public sealed record GenerarDocumentosDigitalesCommand
    (LlaveDocumento LlaveDocumento, CreateDocumentoDigitalOptions Options) : IRequest<string>;

public sealed class GenerarDocumentosDigitalesCommandHandler : IRequestHandler<GenerarDocumentosDigitalesCommand, string>
{
    private readonly ContpaqiComercialConfig _contpaqiComercialConfig;
    private readonly IDocumentoRepository _documentoRepository;
    private readonly IDocumentoService _documentoService;

    public GenerarDocumentosDigitalesCommandHandler(IDocumentoService documentoService,
                                                    IDocumentoRepository documentoRepository,
                                                    IOptions<ContpaqiComercialConfig> contpaqiComercialConfigOptions)
    {
        _documentoService = documentoService;
        _documentoRepository = documentoRepository;
        _contpaqiComercialConfig = contpaqiComercialConfigOptions.Value;
    }

    public Task<string> Handle(GenerarDocumentosDigitalesCommand request, CancellationToken cancellationToken)
    {
        string rutaPlantilla = Path.Combine(_contpaqiComercialConfig.RutaPlantillas, request.Options.NombrePlantilla);
        _documentoService.GenerarDocumentoDigital(request.LlaveDocumento.ConceptoCodigo,
            request.LlaveDocumento.Serie,
            request.LlaveDocumento.Folio,
            request.Options.Tipo,
            rutaPlantilla);

        return Task.FromResult(ArchivoDigitalHelper.GenerarRutaArchivoDigital(request.Options.Tipo,
            _contpaqiComercialConfig.Empresa.Ruta,
            request.LlaveDocumento.Serie,
            request.LlaveDocumento.Folio.ToString()));
    }
}
