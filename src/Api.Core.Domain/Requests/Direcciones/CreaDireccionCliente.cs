using ARSoftware.Contpaqi.Api.Common.Domain;

namespace Api.Core.Domain.Requests.Direcciones;

/// <summary>
///     Solicitud para crear una dirección de un cliente.
/// </summary>
public sealed class CrearDireccionClienteRequest : ContpaqiRequest<CrearDireccionClienteRequestModel, CrearDireccionClienteRequestOptions,
    CrearDireccionClienteResponse>
{
    public CrearDireccionClienteRequest(CrearDireccionClienteRequestModel model, CrearDireccionClienteRequestOptions options) : base(model,
        options)
    {
    }
}

/// <summary>
///     Modelo de la solicitud CreaDireccionClienteRequest.
/// </summary>
public sealed class CrearDireccionClienteRequestModel
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
public sealed class CrearDireccionClienteRequestOptions : ILoadRelatedDataOptions
{
    public bool CargarDatosExtra { get; set; }
}

/// <summary>
///     Respuesta de la solicitud CreaDireccionClienteRequest.
/// </summary>
public sealed class CrearDireccionClienteResponse : ContpaqiResponse<CrearDireccionClienteResponseModel>
{
    public CrearDireccionClienteResponse(CrearDireccionClienteResponseModel model) : base(model)
    {
    }

    public static CrearDireccionClienteResponse CreateInstance(Direccion direccion)
    {
        return new CrearDireccionClienteResponse(new CrearDireccionClienteResponseModel(direccion));
    }
}

/// <summary>
///     Modelo de la respuesta CreaDireccionClienteResponse.
/// </summary>
public sealed class CrearDireccionClienteResponseModel
{
    public CrearDireccionClienteResponseModel(Direccion direccion)
    {
        Direccion = direccion;
    }

    public Direccion Direccion { get; }
}
