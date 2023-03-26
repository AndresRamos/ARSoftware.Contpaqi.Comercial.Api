using Api.Core.Domain.Models;

namespace Api.Core.Domain.Requests;

/// <summary>
///     Solicitud para crear un agente.
/// </summary>
public sealed class CrearAgenteRequest : ApiRequestBase, IApiRequest<CrearAgenteRequestModel, CrearAgenteRequestOptions>
{
    public CrearAgenteRequestModel Model { get; set; } = new();
    public CrearAgenteRequestOptions Options { get; set; } = new();
}

/// <summary>
///     Modelo de la solicitud CrearAgenteRequest.
/// </summary>
public sealed class CrearAgenteRequestModel
{
    public Agente Agente { get; set; } = new();
}

/// <summary>
///     Opciones de la solicitud CrearAgenteRequest.
/// </summary>
public sealed class CrearAgenteRequestOptions : ILoadRelatedDataOptions
{
    /// <summary>
    ///     Opcion para cargar los datos extra de los objectos.
    /// </summary>
    public bool CargarDatosExtra { get; set; }
}

/// <summary>
///     Respuesta de la solicitud CrearAgenteRequest.
/// </summary>
public sealed class CrearAgenteResponse : ApiResponseBase, IApiResponse<CrearAgenteResponseModel>
{
    public CrearAgenteResponseModel Model { get; set; } = new();
}

/// <summary>
///     Modelo de la respuesta CrearAgenteResponse.
/// </summary>
public sealed class CrearAgenteResponseModel
{
    public Agente Agente { get; set; } = new();
}
