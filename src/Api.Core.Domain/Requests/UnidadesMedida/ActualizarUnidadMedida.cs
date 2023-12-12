using ARSoftware.Contpaqi.Api.Common.Domain;

namespace Api.Core.Domain.Requests;

/// <summary>
///     Solicitud para actualizar una unidad de medida.
/// </summary>
public sealed class ActualizarUnidadMedidaRequest : ContpaqiRequest<ActualizarUnidadMedidaRequestModel, ActualizarUnidadMedidaRequestOptions
    , ActualizarUnidadMedidaResponse>
{
    public ActualizarUnidadMedidaRequest(ActualizarUnidadMedidaRequestModel model, ActualizarUnidadMedidaRequestOptions options) :
        base(model, options)
    {
    }
}

/// <summary>
///     Modelo de la solicitud ActualizarUnidadMedidaRequest.
/// </summary>
public sealed class ActualizarUnidadMedidaRequestModel
{
    /// <summary>
    ///     Nombre de la unidad de medida a actualizar.
    /// </summary>
    public string Nombre { get; set; }

    /// <summary>
    ///     Unidad de medida a actualizar.
    /// </summary>
    public UnidadMedida UnidadMedida { get; set; }
}

/// <summary>
///     Opciones de la solicitud ActualizarUnidadMedidaRequest.
/// </summary>
public sealed class ActualizarUnidadMedidaRequestOptions : ILoadRelatedDataOptions
{
    public bool CargarDatosExtra { get; set; }
}

/// <summary>
///     Respuesta de la solicitud ActualizarUnidadMedidaRequest.
/// </summary>
public sealed class ActualizarUnidadMedidaResponse : ContpaqiResponse<ActualizaDireccionClienteResponseModel>
{
    public ActualizarUnidadMedidaResponse(ActualizaDireccionClienteResponseModel model) : base(model)
    {
    }
}

/// <summary>
///     Modelo de la respuesta ActualizarUnidadMedidaResponse.
/// </summary>
public sealed class ActualizarUnidadMedidaResponseModel
{
    public UnidadMedida UnidadMedida { get; set; }
}
