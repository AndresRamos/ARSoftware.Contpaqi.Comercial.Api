using System.Text.Json.Serialization;
using Api.Core.Domain.Models;
using ARSoftware.Contpaqi.Api.Common.Domain;
using ARSoftware.Contpaqi.Comercial.Sdk.Extras.Models.Enums;

namespace Api.Core.Domain.Requests;

/// <summary>
///     Solicitud para generar el XML o PDF del documento.
/// </summary>
public sealed class GenerarDocumentoDigitalRequest : ContpaqiRequest<GenerarDocumentoDigitalRequestModel,
    GenerarDocumentoDigitalRequestOptions, GenerarDocumentoDigitalResponse>
{
    public GenerarDocumentoDigitalRequest(GenerarDocumentoDigitalRequestModel model, GenerarDocumentoDigitalRequestOptions options) :
        base(model, options)
    {
    }
}

/// <summary>
///     Modelo de la solicitud GenerarDocumentoDigitalRequest.
/// </summary>
public sealed class GenerarDocumentoDigitalRequestModel
{
    public LlaveDocumento LlaveDocumento { get; set; } = new();
}

/// <summary>
///     Opciones de la solicitud GenerarDocumentoDigitalRequest.
/// </summary>
public sealed class GenerarDocumentoDigitalRequestOptions
{
    /// <summary>
    ///     Tipo de documento a generar.
    /// </summary>
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public TipoArchivoDigital Tipo { get; set; } = TipoArchivoDigital.Pdf;

    /// <summary>
    ///     Nombre de la plantilla cuando se generar el PDF.
    /// </summary>
    public string NombrePlantilla { get; set; } = string.Empty;
}

/// <summary>
///     Respuesta de la solicitud GenerarDocumentoDigitalRequest.
/// </summary>
public sealed class GenerarDocumentoDigitalResponse : ContpaqiResponse<GenerarDocumentoDigitalResponseModel>
{
    public GenerarDocumentoDigitalResponse(GenerarDocumentoDigitalResponseModel model) : base(model)
    {
    }

    public static GenerarDocumentoDigitalResponse CreateInstance(DocumentoDigital documentoDigital)
    {
        return new GenerarDocumentoDigitalResponse(new GenerarDocumentoDigitalResponseModel(documentoDigital));
    }
}

/// <summary>
///     Modelo de la respuesta GenerarDocumentoDigitalResponse.
/// </summary>
public sealed class GenerarDocumentoDigitalResponseModel
{
    public GenerarDocumentoDigitalResponseModel(DocumentoDigital documentoDigital)
    {
        DocumentoDigital = documentoDigital;
    }

    public DocumentoDigital DocumentoDigital { get; set; }
}
