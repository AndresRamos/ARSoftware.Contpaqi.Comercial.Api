using Api.Core.Domain.Common;
using Api.Core.Domain.Models;

namespace Api.Core.Domain.Requests;

/// <summary>
///     Solicitud para crear un documento.
/// </summary>
public sealed class CreateDocumentoRequest : ApiRequestBase
{
    public Documento Model { get; set; } = new();
    public CreateDocumentoOptions Options { get; set; } = new();
}

public sealed class CreateDocumentoOptions
{
    public bool UsarFechaDelDia { get; set; } = true;
    public bool BuscarSiguienteFolio { get; set; } = true;
    public bool CrearCatalogos { get; set; }
    public bool TimbrarDocumento { get; set; }
    public string ContrasenaCertificado { get; set; } = string.Empty;
}

public sealed class CreateDocumentoResponse : ApiResponseBase
{
    public Documento Model { get; set; } = new();

    public static CreateDocumentoResponse CreateSuccessfull(CreateDocumentoRequest apiRequest, Documento model)
    {
        return new CreateDocumentoResponse { IsSuccess = true, Id = apiRequest.Id, Model = model };
    }

    public static CreateDocumentoResponse CreateFailed(CreateDocumentoRequest apiRequest, string errorMessage)
    {
        return new CreateDocumentoResponse { IsSuccess = false, ErrorMessage = errorMessage, Id = apiRequest.Id };
    }
}
