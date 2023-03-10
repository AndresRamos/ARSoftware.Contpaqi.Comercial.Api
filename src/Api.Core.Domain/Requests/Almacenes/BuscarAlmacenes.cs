using Api.Core.Domain.Models;

namespace Api.Core.Domain.Requests;

/// <inheritdoc cref="IApiRequest" />
public sealed class BuscarAlmacenesRequest : ApiRequestBase, IApiRequest<BuscarAlmacenesRequestModel, BuscarAlmacenesRequestOptions>
{
    public BuscarAlmacenesRequestModel Model { get; set; } = new();
    public BuscarAlmacenesRequestOptions Options { get; set; } = new();
}

public sealed class BuscarAlmacenesRequestModel
{
    public int? Id { get; set; }
    public string? Codigo { get; set; }
}

public sealed class BuscarAlmacenesRequestOptions : ILoadRelatedDataOptions
{
    public bool CargarDatosExtra { get; set; }
}

/// <inheritdoc cref="IApiResponse" />
public sealed class BuscarAlmacenesResponse : ApiResponseBase, IApiResponse<BuscarAlmacenesResponseModel>
{
    public BuscarAlmacenesResponseModel Model { get; set; } = new();
}

public sealed class BuscarAlmacenesResponseModel
{
    public List<Almacen> Almacenes { get; set; } = new();
}
