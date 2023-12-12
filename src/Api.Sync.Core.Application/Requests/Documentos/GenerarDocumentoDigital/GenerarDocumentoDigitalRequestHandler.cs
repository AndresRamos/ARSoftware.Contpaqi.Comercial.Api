using System.Globalization;
using Api.Core.Domain.Models;
using Api.Core.Domain.Requests;
using Api.Sync.Core.Application.Common.Models;
using ARSoftware.Contpaqi.Comercial.Sdk.Abstractions.Enums;
using ARSoftware.Contpaqi.Comercial.Sdk.Extras.Extensions;
using ARSoftware.Contpaqi.Comercial.Sdk.Extras.Helpers;
using ARSoftware.Contpaqi.Comercial.Sdk.Extras.Interfaces;
using MediatR;
using Microsoft.Extensions.Options;

namespace Api.Sync.Core.Application.Requests.Documentos.GenerarDocumentoDigital;

public sealed class GenerarDocumentoDigitalRequestHandler : IRequestHandler<GenerarDocumentoDigitalRequest, GenerarDocumentoDigitalResponse>
{
    private readonly ContpaqiComercialConfig _contpaqiComercialConfig;
    private readonly IDocumentoService _documentoService;
    private readonly IContpaqiSdk _sdk;

    public GenerarDocumentoDigitalRequestHandler(IDocumentoService documentoService,
        IOptions<ContpaqiComercialConfig> contpaqiComercialConfigOptions, IContpaqiSdk sdk)
    {
        _documentoService = documentoService;
        _sdk = sdk;
        _contpaqiComercialConfig = contpaqiComercialConfigOptions.Value;
    }

    public async Task<GenerarDocumentoDigitalResponse> Handle(GenerarDocumentoDigitalRequest request, CancellationToken cancellationToken)
    {
        LlaveDocumento llaveDocumento = request.Model.LlaveDocumento;

        try
        {
            string rutaPlantilla = Path.Combine(_contpaqiComercialConfig.RutaPlantillasPdf, request.Options.NombrePlantilla);

            _documentoService.GenerarDocumentoDigital(llaveDocumento.ConceptoCodigo, llaveDocumento.Serie, llaveDocumento.Folio,
                request.Options.Tipo, rutaPlantilla);

            string rutaDocumento = ArchivoDigitalHelper.GenerarRutaArchivoDigital(request.Options.Tipo,
                _contpaqiComercialConfig.Empresa.Ruta, llaveDocumento.Serie, llaveDocumento.Folio.ToString(CultureInfo.InvariantCulture));

            var documentoDigital = new DocumentoDigital
            {
                Ubicacion = rutaDocumento,
                Nombre = new FileInfo(rutaDocumento).Name,
                Tipo = request.Options.Tipo == TipoArchivoDigital.Pdf ? "application/pdf" : "text/xml",
                Contenido = await File.ReadAllBytesAsync(rutaDocumento, cancellationToken)
            };

            return GenerarDocumentoDigitalResponse.CreateInstance(documentoDigital);
        }
        finally
        {
            // TODO: Utilizar el DocumentoService cuando salga la nueva version donde se pueda desbloquear el documento por llave
            _sdk.fBuscarDocumento(llaveDocumento.ConceptoCodigo, llaveDocumento.Serie, llaveDocumento.Folio.ToString());
            _sdk.fDesbloqueaDocumento().ToResultadoSdk(_sdk).ThrowIfError();
        }
    }
}
