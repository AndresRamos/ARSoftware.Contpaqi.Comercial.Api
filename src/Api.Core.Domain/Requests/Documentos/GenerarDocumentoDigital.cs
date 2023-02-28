using System.Text.Json.Serialization;
using Api.Core.Domain.Models;
using ARSoftware.Contpaqi.Comercial.Sdk.Extras.Models.Enums;

namespace Api.Core.Domain.Requests;

public sealed class GenerarDocumentoDigitalRequest : ApiRequestBase,
    IApiRequest<GenerarDocumentoDigitalRequestModel, GenerarDocumentoDigitalRequestOptions>
{
    public GenerarDocumentoDigitalRequestModel Model { get; set; } = new();
    public GenerarDocumentoDigitalRequestOptions Options { get; set; } = new();
}

public sealed class GenerarDocumentoDigitalRequestModel
{
    public LlaveDocumento LlaveDocumento { get; set; } = new();
}

public sealed class GenerarDocumentoDigitalRequestOptions
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public TipoArchivoDigital Tipo { get; set; } = TipoArchivoDigital.Pdf;

    public string NombrePlantilla { get; set; } = string.Empty;
}

public sealed class GenerarDocumentoDigitalResponse : ApiResponseBase, IApiResponse<GenerarDocumentoDigitalResponseModel>
{
    public GenerarDocumentoDigitalResponseModel Model { get; set; } = new();
}

public sealed class GenerarDocumentoDigitalResponseModel
{
    public DocumentoDigital DocumentoDigital { get; set; } = new();
}
