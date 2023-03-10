using Api.Core.Domain.Models;

namespace Api.Core.Domain.Requests;

/// <inheritdoc cref="IApiRequest" />
public sealed class BuscarConceptosRequest : ApiRequestBase, IApiRequest<BuscarConceptosRequestModel, BuscarConceptosRequestOptions>
{
    public BuscarConceptosRequestModel Model { get; set; } = new();
    public BuscarConceptosRequestOptions Options { get; set; } = new();
}

public sealed class BuscarConceptosRequestModel
{
    public int? Id { get; set; }
    public string? Codigo { get; set; }
}

public sealed class BuscarConceptosRequestOptions : ILoadRelatedDataOptions
{
    public bool CargarDatosExtra { get; set; }
}

/// <inheritdoc cref="IApiResponse" />
public sealed class BuscarConceptosResponse : ApiResponseBase, IApiResponse<BuscarConceptosResponseModel>
{
    public BuscarConceptosResponseModel Model { get; set; } = new();
}

public sealed class BuscarConceptosResponseModel
{
    public List<Concepto> Conceptos { get; set; } = new();
}
