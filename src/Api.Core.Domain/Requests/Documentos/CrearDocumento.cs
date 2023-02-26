using Api.Core.Domain.Models;

namespace Api.Core.Domain.Requests;

/// <inheritdoc cref="IApiRequest" />
public sealed class CrearDocumentoRequest : ApiRequestBase, IApiRequest<CrearDocumentoRequestModel, CrearDocumentoRequestOptions>
{
    public CrearDocumentoRequestModel Model { get; set; } = new();
    public CrearDocumentoRequestOptions Options { get; set; } = new();
}

public sealed class CrearDocumentoRequestModel
{
    public Documento Documento { get; set; } = new();
}

public sealed class CrearDocumentoRequestOptions
{
    public bool UsarFechaDelDia { get; set; } = true;
    public bool BuscarSiguienteFolio { get; set; } = true;
    public bool CrearCatalogos { get; set; }
}

/// <inheritdoc cref="IApiResponse" />
public sealed class CrearDocumentoResponse : ApiResponseBase, IApiResponse<CrearDocumentoResponseModel>
{
    public CrearDocumentoResponseModel Model { get; set; } = new();
}

public sealed class CrearDocumentoResponseModel
{
    public Documento Documento { get; set; } = new();
}
