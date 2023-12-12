using ARSoftware.Contpaqi.Api.Common.Domain;

namespace Api.Core.Domain.Requests;

/// <summary>
///     Solicitud para eliminar una unidad de medida.
/// </summary>
public sealed class EliminarUnidadMedidaRequest : ContpaqiRequest<EliminarUnidadMedidaRequestModel, EliminarUnidadMedidaRequestOptions,
    EliminarUnidadMedidaResponse>
{
    public EliminarUnidadMedidaRequest(EliminarUnidadMedidaRequestModel model, EliminarUnidadMedidaRequestOptions options) : base(model,
        options)
    {
    }
}

/// <summary>
///     Modelo de la solicitud EliminarUnidadMedidaRequest.
/// </summary>
public sealed class EliminarUnidadMedidaRequestModel
{
    /// <summary>
    ///     Nombre de la unidad de medida.
    /// </summary>
    public string NombreUnidad { get; set; }
}

/// <summary>
///     Opciones de la solicitud EliminarUnidadMedidaRequest.
/// </summary>
public sealed class EliminarUnidadMedidaRequestOptions
{
}

/// <summary>
///     Respuesta a la solicitud EliminarUnidadMedidaRequest.
/// </summary>
public sealed class EliminarUnidadMedidaResponse : ContpaqiResponse<EliminarUnidadMedidaResponseModel>
{
    public EliminarUnidadMedidaResponse(EliminarUnidadMedidaResponseModel model) : base(model)
    {
    }
}

/// <summary>
///     Modelo de la respuesta EliminarUnidadMedidaResponse.
/// </summary>
public sealed class EliminarUnidadMedidaResponseModel
{
}
