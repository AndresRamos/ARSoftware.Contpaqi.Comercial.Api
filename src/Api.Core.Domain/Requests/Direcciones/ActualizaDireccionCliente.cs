using ARSoftware.Contpaqi.Api.Common.Domain;

namespace Api.Core.Domain.Requests;

/// <summary>
///     Solicitud para actualizar una dirección de un cliente.
/// </summary>
public sealed class ActualizaDireccionClienteRequest : ContpaqiRequest<ActualizaDireccionClienteRequestModel,
    ActualizaDireccionClienteRequestOptions, ActualizaDireccionClienteResponse>
{
    public ActualizaDireccionClienteRequest(ActualizaDireccionClienteRequestModel model, ActualizaDireccionClienteRequestOptions options) :
        base(model, options)
    {
    }
}

/// <summary>
///     Modelo de la solicitud ActualizaDireccionClienteRequest.
/// </summary>
public sealed class ActualizaDireccionClienteRequestModel
{
    /// <summary>
    ///     Código del cliente al que se le actualizara la dirección.
    /// </summary>
    public string CodigoCliente { get; set; }

    /// <summary>
    ///     Direccion a actualizar.
    /// </summary>
    public Direccion Direccion { get; set; } = new();
}

/// <summary>
///     Opciones de la solicitud ActualizaDireccionClienteRequest.
/// </summary>
public sealed class ActualizaDireccionClienteRequestOptions : ILoadRelatedDataOptions
{
    public bool CargarDatosExtra { get; set; }
}

/// <summary>
///     Respuesta de la solicitud ActualizaDireccionClienteRequest.
/// </summary>
public sealed class ActualizaDireccionClienteResponse : ContpaqiResponse<ActualizaDireccionClienteResponseModel>
{
    public ActualizaDireccionClienteResponse(ActualizaDireccionClienteResponseModel model) : base(model)
    {
    }

    public static ActualizaDireccionClienteResponse CreateInstance(Direccion direccion)
    {
        return new ActualizaDireccionClienteResponse(new ActualizaDireccionClienteResponseModel(direccion));
    }
}

/// <summary>
///     Modelo de la respuesta ActualizaDireccionClienteResponse.
/// </summary>
public sealed class ActualizaDireccionClienteResponseModel
{
    public ActualizaDireccionClienteResponseModel(Direccion direccion)
    {
        Direccion = direccion;
    }

    /// <summary>
    ///     Direccion actualizada.
    /// </summary>
    public Direccion Direccion { get; }
}
