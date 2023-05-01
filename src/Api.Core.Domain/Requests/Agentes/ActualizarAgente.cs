using Api.Core.Domain.Models;
using ARSoftware.Contpaqi.Api.Common.Domain;

namespace Api.Core.Domain.Requests;

/// <summary>
///     Solicitud para actualizar un agente.
/// </summary>
public sealed class
    ActualizarAgenteRequest : ContpaqiRequest<ActualizarAgenteRequestModel, ActualizarAgenteRequestOptions, ActualizarAgenteResponse>
{
    public ActualizarAgenteRequest(ActualizarAgenteRequestModel model, ActualizarAgenteRequestOptions options) : base(model, options)
    {
    }
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
/// <inheritdoc cref="ILoadRelatedDataOptions" />
public sealed class ActualizarAgenteRequestOptions : ILoadRelatedDataOptions
{
    public bool CargarDatosExtra { get; set; }
}

/// <summary>
///     Respuesta de la solicitud ActualizarAgenteRequest.
/// </summary>
public sealed class ActualizarAgenteResponse : ContpaqiResponse<ActualizarAgenteResponseModel>
{
    public ActualizarAgenteResponse(ActualizarAgenteResponseModel model) : base(model)
    {
    }

    public static ActualizarAgenteResponse CreateInstance(Agente agente)
    {
        return new ActualizarAgenteResponse(new ActualizarAgenteResponseModel(agente));
    }
}

/// <summary>
///     Modelo de la respuesta ActualizarAgenteResponse.
/// </summary>
public sealed class ActualizarAgenteResponseModel
{
    public ActualizarAgenteResponseModel(Agente agente)
    {
        Agente = agente;
    }

    public Agente Agente { get; set; }
}
