using Api.Core.Domain.Models;
using ARSoftware.Contpaqi.Api.Common.Domain;

namespace Api.Core.Domain.Requests;

/// <summary>
///     Solicitud para crear un cliente.
/// </summary>
public sealed class CrearClienteRequest : ContpaqiRequest<CrearClienteRequestModel, CrearClienteRequestOptions, CrearClienteResponse>
{
    public CrearClienteRequest(CrearClienteRequestModel model, CrearClienteRequestOptions options) : base(model, options)
    {
    }
}

/// <summary>
///     Modelo de la solicitud CrearClienteRequest.
/// </summary>
public sealed class CrearClienteRequestModel
{
    public Cliente Cliente { get; set; } = new();
}

/// <summary>
///     Opciones de la solicitud CrearClienteRequest.
/// </summary>
/// <inheritdoc cref="ILoadRelatedDataOptions" />
public sealed class CrearClienteRequestOptions : ILoadRelatedDataOptions
{
    public bool CargarDatosExtra { get; set; }
}

/// <summary>
///     Respuesta de la solicitud CrearClienteRequest.
/// </summary>
public sealed class CrearClienteResponse : ContpaqiResponse<CrearClienteResponseModel>
{
    public CrearClienteResponse(CrearClienteResponseModel model) : base(model)
    {
    }

    public static CrearClienteResponse CreateInstance(Cliente cliente)
    {
        return new CrearClienteResponse(new CrearClienteResponseModel(cliente));
    }
}

/// <summary>
///     Modelo de la respuesta CrearClienteResponse
/// </summary>
public sealed class CrearClienteResponseModel
{
    public CrearClienteResponseModel(Cliente cliente)
    {
        Cliente = cliente;
    }

    public Cliente Cliente { get; set; }
}
