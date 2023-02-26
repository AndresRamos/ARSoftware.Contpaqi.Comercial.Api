using System.Text.Json.Serialization;
using Api.Core.Domain.Models;
using ARSoftware.Contpaqi.Comercial.Sdk.Extras.Models.Enums;

namespace Api.Core.Domain.Requests;

public sealed class CrearDocumentoDigitalRequest : ApiRequestBase,
    IApiRequest<CrearDocumentoDigitalRequestModel, CrearDocumentoDigitalRequestOptions>
{
    public CrearDocumentoDigitalRequestModel Model { get; set; } = new();
    public CrearDocumentoDigitalRequestOptions Options { get; set; } = new();
}

public sealed class CrearDocumentoDigitalRequestModel
{
    public LlaveDocumento LlaveDocumento { get; set; } = new();
}

public sealed class CrearDocumentoDigitalRequestOptions
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public TipoArchivoDigital Tipo { get; set; } = TipoArchivoDigital.Pdf;

    public string NombrePlantilla { get; set; } = string.Empty;
}

public sealed class CrearDocumentoDigitalResponse : ApiResponseBase, IApiResponse<CrearDocumentoDigitalResponseModel>
{
    public CrearDocumentoDigitalResponseModel Model { get; set; } = new();
}

public sealed class CrearDocumentoDigitalResponseModel
{
    public DocumentoDigital DocumentoDigital { get; set; } = new();
}
