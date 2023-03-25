using Api.Core.Domain.Models;

namespace Api.Core.Domain.Requests;

/// <inheritdoc cref="IApiRequest" />
public sealed class BuscarDocumentosRequest : ApiRequestBase, IApiRequest<BuscarDocumentosRequestModel, BuscarDocumentosRequestOptions>
{
    public BuscarDocumentosRequestModel Model { get; set; } = new();
    public BuscarDocumentosRequestOptions Options { get; set; } = new();
}

public sealed class BuscarDocumentosRequestModel
{
    public int? Id { get; set; }
    public LlaveDocumento? Llave { get; set; }
    public string? ConceptoCodigo { get; set; }
    public DateOnly? FechaInicio { get; set; }
    public DateOnly? FechaFin { get; set; }
    public string? ClienteCodigo { get; set; }
    public string? SqlQuery { get; set; }
}

public sealed class BuscarDocumentosRequestOptions : ILoadRelatedDataOptions
{
    public bool CargarDatosExtra { get; set; }
}

/// <inheritdoc cref="IApiResponse" />
public sealed class BuscarDocumentosResponse : ApiResponseBase, IApiResponse<BuscarDocumentosResponseModel>
{
    public BuscarDocumentosResponseModel Model { get; set; } = new();
}

public sealed class BuscarDocumentosResponseModel
{
    public List<Documento> Documentos { get; set; } = new();
}
