using System.Text.Json.Serialization;
using Api.Core.Domain.Models;
using ARSoftware.Contpaqi.Comercial.Sdk.Extras.Models.Enums;

namespace Api.Core.Domain.Requests;

/// <summary>
///     Solicitud para generar el XML o PDF del documento.
/// </summary>
public sealed class GenerarDocumentoDigitalRequest : ApiRequestBase,
    IApiRequest<GenerarDocumentoDigitalRequestModel, GenerarDocumentoDigitalRequestOptions>
{
    public GenerarDocumentoDigitalRequestModel Model { get; set; } = new();
    public GenerarDocumentoDigitalRequestOptions Options { get; set; } = new();
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
public sealed class GenerarDocumentoDigitalResponse : ApiResponseBase, IApiResponse<GenerarDocumentoDigitalResponseModel>
{
    public GenerarDocumentoDigitalResponseModel Model { get; set; } = new();
}

/// <summary>
///     Modelo de la respuesta GenerarDocumentoDigitalResponse.
/// </summary>
public sealed class GenerarDocumentoDigitalResponseModel
{
    public DocumentoDigital DocumentoDigital { get; set; } = new();
}
