using Api.Core.Domain.Models;

namespace Api.Core.Domain.Requests;

/// <inheritdoc cref="IApiRequest" />
public sealed class BuscarClientesRequest : ApiRequestBase, IApiRequest<BuscarClientesRequestModel, BuscarClientesRequestOptions>
{
    public BuscarClientesRequestModel Model { get; set; } = new();
    public BuscarClientesRequestOptions Options { get; set; } = new();
}

public sealed class BuscarClientesRequestModel
{
    public int? Id { get; set; }
    public string? Codigo { get; set; }
}

public sealed class BuscarClientesRequestOptions : ILoadRelatedDataOptions
{
    public bool CargarDatosExtra { get; set; }
}

/// <inheritdoc cref="IApiResponse" />
public sealed class BuscarClientesResponse : ApiResponseBase, IApiResponse<BuscarClientesResponseModel>
{
    public BuscarClientesResponseModel Model { get; set; } = new();
}

public sealed class BuscarClientesResponseModel
{
    public List<Cliente> Clientes { get; set; } = new();
}
