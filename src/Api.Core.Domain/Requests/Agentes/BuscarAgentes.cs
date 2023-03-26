using Api.Core.Domain.Models;

namespace Api.Core.Domain.Requests;

/// <summary>
///     Solicitud para buscar agentes.
/// </summary>
public sealed class BuscarAgentesRequest : ApiRequestBase, IApiRequest<BuscarAgentesRequestModel, BuscarAgentesRequestOptions>
{
    public BuscarAgentesRequestModel Model { get; set; } = new();
    public BuscarAgentesRequestOptions Options { get; set; } = new();
}

/// <summary>
///     Modelo de la solicitud BuscarAgentesRequest.
/// </summary>
public sealed class BuscarAgentesRequestModel
{
    /// <summary>
    ///     Parametro para buscar agentes por id.
    /// </summary>
    public int? Id { get; set; }

    /// <summary>
    ///     Parametro para buscar agentes por codigo.
    /// </summary>
    public string? Codigo { get; set; }
}

/// <summary>
///     Opciones de la solicitud BuscarAgentesRequest.
/// </summary>
/// <inheritdoc cref="ILoadRelatedDataOptions" />
public sealed class BuscarAgentesRequestOptions : ILoadRelatedDataOptions
{
    public bool CargarDatosExtra { get; set; }
}

/// <summary>
///     Respuesta de la solicitud BuscarAgentesRequest.
/// </summary>
public sealed class BuscarAgentesResponse : ApiResponseBase, IApiResponse<BuscarAgentesResponseModel>
{
    public BuscarAgentesResponseModel Model { get; set; } = new();
}

/// <summary>
///     Modelo de la respuesta BuscarAgentesResponse.
/// </summary>
public sealed class BuscarAgentesResponseModel
{
    public List<Agente> Agentes { get; set; } = new();
}
