using ARSoftware.Contpaqi.Api.Common.Domain;

namespace Api.Core.Domain.Requests;

/// <summary>
///     Solicitud para eliminar un cliente.
/// </summary>
public sealed class
    EliminarClienteRequest : ContpaqiRequest<EliminarClienteRequestModel, EliminarClienteRequestOptions, EliminarClienteResponse>
{
    public EliminarClienteRequest(EliminarClienteRequestModel model, EliminarClienteRequestOptions options) : base(model, options)
    {
    }
}

/// <summary>
///     Modelo de la solicitud EliminarClienteRequest.
/// </summary>
public sealed class EliminarClienteRequestModel
{
    /// <summary>
    ///     Codigo del cliente a eliminar.
    /// </summary>
    public string CodigoCliente { get; set; } = string.Empty;
}

/// <summary>
///     Opciones de la solicitud EliminarClienteRequest.
/// </summary>
public sealed class EliminarClienteRequestOptions
{
}

/// <summary>
///     Respuesta de la solicitud EliminarClienteRequest.
/// </summary>
public sealed class EliminarClienteResponse : ContpaqiResponse<EliminarClienteResponseModel>
{
    public EliminarClienteResponse(EliminarClienteResponseModel model) : base(model)
    {
    }

    public static EliminarClienteResponse CreateInstance()
    {
        return new EliminarClienteResponse(new EliminarClienteResponseModel());
    }
}

/// <summary>
///     Modelo de la respuesta EliminarClienteResponse.
/// </summary>
public sealed class EliminarClienteResponseModel
{
}
