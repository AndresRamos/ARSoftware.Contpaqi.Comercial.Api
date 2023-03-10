using Api.Core.Domain.Models;

namespace Api.Core.Domain.Requests;

/// <inheritdoc cref="IApiRequest" />
public sealed class BuscarAgentesRequest : ApiRequestBase, IApiRequest<BuscarAgentesRequestModel, BuscarAgentesRequestOptions>
{
    public BuscarAgentesRequestModel Model { get; set; } = new();
    public BuscarAgentesRequestOptions Options { get; set; } = new();
}

public sealed class BuscarAgentesRequestModel
{
    public int? Id { get; set; }
    public string? Codigo { get; set; }
}

public sealed class BuscarAgentesRequestOptions : ILoadRelatedDataOptions
{
    public bool CargarDatosExtra { get; set; }
}

/// <inheritdoc cref="IApiResponse" />
public sealed class BuscarAgentesResponse : ApiResponseBase, IApiResponse<BuscarAgentesResponseModel>
{
    public BuscarAgentesResponseModel Model { get; set; } = new();
}

public sealed class BuscarAgentesResponseModel
{
    public List<Agente> Agentes { get; set; } = new();
}
