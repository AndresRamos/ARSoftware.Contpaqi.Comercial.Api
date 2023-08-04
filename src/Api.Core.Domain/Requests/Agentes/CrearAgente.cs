using ARSoftware.Contpaqi.Api.Common.Domain;

namespace Api.Core.Domain.Requests;

/// <summary>
///     Solicitud para crear un agente.
/// </summary>
public sealed class CrearAgenteRequest : ContpaqiRequest<CrearAgenteRequestModel, CrearAgenteRequestOptions, CrearAgenteResponse>
{
    public CrearAgenteRequest(CrearAgenteRequestModel model, CrearAgenteRequestOptions options) : base(model, options)
    {
    }
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
/// <inheritdoc cref="ILoadRelatedDataOptions" />
public sealed class CrearAgenteRequestOptions : ILoadRelatedDataOptions
{
    public bool CargarDatosExtra { get; set; }
}

/// <summary>
///     Respuesta de la solicitud CrearAgenteRequest.
/// </summary>
public sealed class CrearAgenteResponse : ContpaqiResponse<CrearAgenteResponseModel>
{
    public CrearAgenteResponse(CrearAgenteResponseModel model) : base(model)
    {
    }

    public static CrearAgenteResponse CreateInstance(Agente agente)
    {
        return new CrearAgenteResponse(new CrearAgenteResponseModel(agente));
    }
}

/// <summary>
///     Modelo de la respuesta CrearAgenteResponse.
/// </summary>
public sealed class CrearAgenteResponseModel
{
    public CrearAgenteResponseModel(Agente agente)
    {
        Agente = agente;
    }

    public Agente Agente { get; set; }
}
