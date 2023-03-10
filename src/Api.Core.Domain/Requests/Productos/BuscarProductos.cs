using Api.Core.Domain.Models;

namespace Api.Core.Domain.Requests;

/// <inheritdoc cref="IApiRequest" />
public sealed class BuscarProductosRequest : ApiRequestBase, IApiRequest<BuscarProductosRequestModel, BuscarProductosRequestOptions>
{
    public BuscarProductosRequestModel Model { get; set; } = new();
    public BuscarProductosRequestOptions Options { get; set; } = new();
}

public sealed class BuscarProductosRequestModel
{
    public int? Id { get; set; }
    public string? Codigo { get; set; }
}

public sealed class BuscarProductosRequestOptions : ILoadRelatedDataOptions
{
    public bool CargarDatosExtra { get; set; }
}

/// <inheritdoc cref="IApiResponse" />
public sealed class BuscarProductosResponse : ApiResponseBase, IApiResponse<BuscarProductosResponseModel>
{
    public BuscarProductosResponseModel Model { get; set; } = new();
}

public sealed class BuscarProductosResponseModel
{
    public List<Producto> Productos { get; set; } = new();
}
