using Api.Core.Domain.Models;
using ARSoftware.Contpaqi.Api.Common.Domain;

namespace Api.Core.Domain.Requests;

/// <summary>
///     Solicitud para crear un almacen.
/// </summary>
public sealed class CrearAlmacenRequest : ContpaqiRequest<CrearAlmacenRequestModel, CrearAlmacenRequestOptions, CrearAlmacenResponse>
{
    public CrearAlmacenRequest(CrearAlmacenRequestModel model, CrearAlmacenRequestOptions options) : base(model, options)
    {
    }
}

/// <summary>
///     Modelo de la solicitud CrearAlmacenRequest.
/// </summary>
public sealed class CrearAlmacenRequestModel
{
    public Almacen Almacen { get; set; } = new();
}

/// <summary>
///     Opciones de la solicitud CrearAlmacenRequest.
/// </summary>
/// <inheritdoc cref="ILoadRelatedDataOptions" />
public sealed class CrearAlmacenRequestOptions : ILoadRelatedDataOptions
{
    public bool CargarDatosExtra { get; set; }
}

/// <summary>
///     Respuesta de la solicitud CrearAlmacenRequest.
/// </summary>
public sealed class CrearAlmacenResponse : ContpaqiResponse<CrearAlmacenResponseModel>
{
    public CrearAlmacenResponse(CrearAlmacenResponseModel model) : base(model)
    {
    }

    public static CrearAlmacenResponse CreateInstance(Almacen almacen)
    {
        return new CrearAlmacenResponse(new CrearAlmacenResponseModel(almacen));
    }
}

/// <summary>
///     Modelo de la respuesta CrearAlmacenResponse.
/// </summary>
public sealed class CrearAlmacenResponseModel
{
    public CrearAlmacenResponseModel(Almacen almacen)
    {
        Almacen = almacen;
    }

    public Almacen Almacen { get; set; }
}
