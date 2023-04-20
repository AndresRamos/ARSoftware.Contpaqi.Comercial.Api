using Api.Core.Domain.Models;

namespace Api.Core.Domain.Requests;

/// <summary>
///     Solicitud para actualizar un cliente.
/// </summary>
public sealed class ActualizarClienteRequest : IContpaqiRequest<ActualizarClienteRequestModel, ActualizarClienteRequestOptions>
{
    public ActualizarClienteRequestModel Model { get; set; } = new();
    public ActualizarClienteRequestOptions Options { get; set; } = new();
}

/// <summary>
///     Modelo de la solicitud ActualizarClienteRequest.
/// </summary>
public sealed class ActualizarClienteRequestModel
{
    /// <summary>
    ///     Codigo del cliente a actualizar.
    /// </summary>
    public string CodigoCliente { get; set; } = string.Empty;

    /// <summary>
    ///     Datos a actualizar. Los nombres deben ser igual a los de la base de datos.
    /// </summary>
    public Dictionary<string, string> DatosCliente { get; set; } = new();
}

/// <summary>
///     Opciones de la solicitud ActualizarClienteRequest.
/// </summary>
/// <inheritdoc cref="ILoadRelatedDataOptions" />
public sealed class ActualizarClienteRequestOptions : ILoadRelatedDataOptions
{
    public bool CargarDatosExtra { get; set; }
}

/// <summary>
///     Respuesta de la solicitud ActualizarClienteRequest.
/// </summary>
public sealed class ActualizarClienteResponse : IContpaqiResponse<ActualizarClienteResponseModel>
{
    public ActualizarClienteResponseModel Model { get; set; } = new();
}

/// <summary>
///     Modelo de la respuesta ActualizarClienteResponse.
/// </summary>
public sealed class ActualizarClienteResponseModel
{
    public Cliente Cliente { get; set; } = new();
}
