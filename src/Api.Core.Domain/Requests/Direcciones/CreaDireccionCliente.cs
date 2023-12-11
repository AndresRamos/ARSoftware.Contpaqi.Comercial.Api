using ARSoftware.Contpaqi.Api.Common.Domain;

namespace Api.Core.Domain.Requests.Direcciones;

/// <summary>
///     Solicitud para crear una dirección de un cliente.
/// </summary>
public sealed class CreaDireccionClienteRequest : ContpaqiRequest<CreaDireccionClienteRequestModel, CreaDireccionClienteRequestOptions,
    CreaDireccionClienteResponse>
{
    public CreaDireccionClienteRequest(CreaDireccionClienteRequestModel model, CreaDireccionClienteRequestOptions options) : base(model,
        options)
    {
    }
}

/// <summary>
///     Modelo de la solicitud CreaDireccionClienteRequest.
/// </summary>
public sealed class CreaDireccionClienteRequestModel
{
    /// <summary>
    ///     Código del cliente al que se le creara la dirección.
    /// </summary>
    public string CodigoCliente { get; set; }

    /// <summary>
    ///     Direccion a crear.
    /// </summary>
    public Direccion Direccion { get; set; } = new();
}

/// <summary>
///     Opciones de la solicitud CreaDireccionClienteRequest.
/// </summary>
public sealed class CreaDireccionClienteRequestOptions : ILoadRelatedDataOptions
{
    public bool CargarDatosExtra { get; set; }
}

/// <summary>
///     Respuesta de la solicitud CreaDireccionClienteRequest.
/// </summary>
public sealed class CreaDireccionClienteResponse : ContpaqiResponse<CreaDireccionClienteResponseModel>
{
    public CreaDireccionClienteResponse(CreaDireccionClienteResponseModel model) : base(model)
    {
    }

    public static CreaDireccionClienteResponse CreateInstance(Direccion direccion)
    {
        return new CreaDireccionClienteResponse(new CreaDireccionClienteResponseModel(direccion));
    }
}

/// <summary>
///     Modelo de la respuesta CreaDireccionClienteResponse.
/// </summary>
public sealed class CreaDireccionClienteResponseModel
{
    public CreaDireccionClienteResponseModel(Direccion direccion)
    {
        Direccion = direccion;
    }

    public Direccion Direccion { get; }
}
