using Api.Core.Domain.Common;
using Api.Core.Domain.Factories;
using Api.Core.Domain.Models;
using Api.Core.Domain.Requests;
using Api.Sync.Core.Application.Common.Models;
using ARSoftware.Contpaqi.Comercial.Sdk.Extras.Helpers;
using ARSoftware.Contpaqi.Comercial.Sdk.Extras.Interfaces;
using ARSoftware.Contpaqi.Comercial.Sdk.Extras.Models.Enums;
using MediatR;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Api.Sync.Core.Application.Documentos;

public sealed class CrearDocumentoDigitalRequestHandler : IRequestHandler<CrearDocumentoDigitalRequest, ApiResponseBase>
{
    private readonly ContpaqiComercialConfig _contpaqiComercialConfig;
    private readonly IDocumentoService _documentoService;
    private readonly ILogger _logger;

    public CrearDocumentoDigitalRequestHandler(IDocumentoService documentoService,
                                               IOptions<ContpaqiComercialConfig> contpaqiComercialConfigOptions,
                                               ILogger<CrearDocumentoDigitalRequest> logger)
    {
        _documentoService = documentoService;
        _logger = logger;
        _contpaqiComercialConfig = contpaqiComercialConfigOptions.Value;
    }

    public Task<ApiResponseBase> Handle(CrearDocumentoDigitalRequest request, CancellationToken cancellationToken)
    {
        LlaveDocumento llaveDocumento = request.Model.LlaveDocumento;
        try
        {
            string rutaPlantilla = Path.Combine(_contpaqiComercialConfig.RutaPlantillas, request.Options.NombrePlantilla);
            _documentoService.GenerarDocumentoDigital(llaveDocumento.ConceptoCodigo,
                llaveDocumento.Serie,
                llaveDocumento.Folio,
                request.Options.Tipo,
                rutaPlantilla);

            string? rutaDocumento = ArchivoDigitalHelper.GenerarRutaArchivoDigital(request.Options.Tipo,
                _contpaqiComercialConfig.Empresa.Ruta,
                llaveDocumento.Serie,
                llaveDocumento.Folio.ToString());

            var responseModel = new CrearDocumentoDigitalResponseModel
            {
                DocumentoDigital = new DocumentoDigital
                {
                    Ubicacion = rutaDocumento,
                    Nombre = new FileInfo(rutaDocumento).Name,
                    Tipo = request.Options.Tipo == TipoArchivoDigital.Pdf ? "application/pdf" : "text/xml"
                }
            };

            return Task.FromResult<ApiResponseBase>(
                ApiResponseFactory.CreateSuccessfull<CrearDocumentoDigitalResponse, CrearDocumentoDigitalResponseModel>(request.Id,
                    responseModel));
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error al crear el documento digital.");
            return Task.FromResult<ApiResponseBase>(ApiResponseFactory.CreateFailed<CrearDocumentoDigitalResponse>(request.Id, e.Message));
        }
    }
}
