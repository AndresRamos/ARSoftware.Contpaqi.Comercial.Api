using Api.Core.Domain.Models;
using ARSoftware.Contpaqi.Api.Common.Domain;

namespace Api.Core.Domain.Requests;

/// <summary>
///     Solicitud para actualizar un almacen.
/// </summary>
public sealed class ActualizarAlmacenRequest :
    ContpaqiRequest<ActualizarAlmacenRequestModel, ActualizarAlmacenRequestOptions, ActualizarAlmacenResponse>
{
    public ActualizarAlmacenRequest(ActualizarAlmacenRequestModel model, ActualizarAlmacenRequestOptions options) : base(model, options)
    {
    }
}

/// <summary>
///     Modelo de la solicitud ActualizarAlmacenRequest.
/// </summary>
public sealed class ActualizarAlmacenRequestModel
{
    /// <summary>
    ///     Codigo del almacen a actualizar.
    /// </summary>
    public string CodigoAlmacen { get; set; } = string.Empty;

    /// <summary>
    ///     Datos a actualizar. Los nombres deben ser igual a los de la base de datos.
    /// </summary>
    public Dictionary<string, string> DatosAlmacen { get; set; } = new();
}

/// <summary>
///     Opciones de la solicitud ActualizarAlmacenRequest.
/// </summary>
/// <inheritdoc cref="ILoadRelatedDataOptions" />
public sealed class ActualizarAlmacenRequestOptions : ILoadRelatedDataOptions
{
    public bool CargarDatosExtra { get; set; }
}

/// <summary>
///     Respuesta de la solicitud ActualizarAlmacenRequest.
/// </summary>
public sealed class ActualizarAlmacenResponse : ContpaqiResponse<ActualizarAlmacenResponseModel>
{
    public ActualizarAlmacenResponse(ActualizarAlmacenResponseModel model) : base(model)
    {
    }

    public static ActualizarAlmacenResponse CreateInstance(Almacen almacen)
    {
        return new ActualizarAlmacenResponse(new ActualizarAlmacenResponseModel(almacen));
    }
}

/// <summary>
///     Modelo de la respuesta ActualizarAlmacenResponse.
/// </summary>
public sealed class ActualizarAlmacenResponseModel
{
    public ActualizarAlmacenResponseModel(Almacen almacen)
    {
        Almacen = almacen;
    }

    public Almacen Almacen { get; set; }
}
