using Api.Core.Domain.Models;

namespace Api.Core.Domain.Requests;

/// <summary>
///     Solicitud para actualizar un agente.
/// </summary>
public sealed class ActualizarAgenteRequest : ApiRequestBase, IApiRequest<ActualizarAgenteRequestModel, ActualizarAgenteRequestOptions>
{
    public ActualizarAgenteRequestModel Model { get; set; } = new();

    public ActualizarAgenteRequestOptions Options { get; set; } = new();
}

/// <summary>
///     Modelo de la solicitud ActualizarAgenteRequest.
/// </summary>
public sealed class ActualizarAgenteRequestModel
{
    /// <summary>
    ///     Codigo del agente a actualizar
    /// </summary>
    public string CodigoAgente { get; set; } = string.Empty;

    /// <summary>
    ///     Datos a actualizar. Los nombres deben ser igual a los de la base de datos.
    /// </summary>
    public Dictionary<string, string> DatosAgente { get; set; } = new();
}

/// <summary>
///     Opciones de la solicitud ActualizarAgenteRequest.
/// </summary>
public sealed class ActualizarAgenteRequestOptions : ILoadRelatedDataOptions
{
    /// <summary>
    ///     Opcion para cargar los datos extra de los objectos.
    /// </summary>
    public bool CargarDatosExtra { get; set; }
}

/// <summary>
///     Respuesta de la solicitud ActualizarAgenteRequest.
/// </summary>
public sealed class ActualizarAgenteResponse : ApiResponseBase, IApiResponse<ActualizarAgenteResponseModel>
{
    public ActualizarAgenteResponseModel Model { get; set; } = new();
}

/// <summary>
///     Modelo de la respuesta ActualizarAgenteResponse.
/// </summary>
public sealed class ActualizarAgenteResponseModel
{
    public Agente Agente { get; set; } = new();
}
