using System.Text.Json.Serialization;
using Api.Core.Domain.Common;
using Api.Core.Domain.Models;
using ARSoftware.Contpaqi.Comercial.Sdk.Extras.Models.Enums;

namespace Api.Core.Domain.Requests;

public sealed class CreateDocumentoDigitalRequest : ApiRequestBase
{
    public LlaveDocumento Model { get; set; } = new();
    public CreateDocumentoDigitalOptions Options { get; set; } = new();
}

public sealed class CreateDocumentoDigitalOptions
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public TipoArchivoDigital Tipo { get; set; } = TipoArchivoDigital.Pdf;

    public string NombrePlantilla { get; set; } = string.Empty;
}

public sealed class CreateDocumentoDigitalResponse : ApiResponseBase
{
    public DocumentoDigital Model { get; set; } = new();

    public static CreateDocumentoDigitalResponse CreateSuccessfull(CreateDocumentoDigitalRequest apiRequest, DocumentoDigital model)
    {
        return new CreateDocumentoDigitalResponse { IsSuccess = true, Id = apiRequest.Id, Model = model };
    }

    public static CreateDocumentoDigitalResponse CreateFailed(CreateDocumentoDigitalRequest apiRequest, string errorMessage)
    {
        return new CreateDocumentoDigitalResponse { IsSuccess = false, ErrorMessage = errorMessage, Id = apiRequest.Id };
    }
}
