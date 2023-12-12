using ARSoftware.Contpaqi.Api.Common.Domain;

namespace Api.Core.Domain.Requests;

/// <summary>
///     Solicitud para crear una unidad de medida.
/// </summary>
public sealed class
    CrearUnidadMedidaRequest : ContpaqiRequest<CrearUnidadMedidaRequestModel, CrearUnidadMedidaRequestOptions, CrearUnidadMedidaResponse>
{
    public CrearUnidadMedidaRequest(CrearUnidadMedidaRequestModel model, CrearUnidadMedidaRequestOptions options) : base(model, options)
    {
    }
}

/// <summary>
///     Modelo de la solicitud CrearUnidadMedidaRequest.
/// </summary>
public sealed class CrearUnidadMedidaRequestModel
{
    public UnidadMedida UnidadMedida { get; set; }
}

/// <summary>
///     Opciones de la solicitud CrearUnidadMedidaRequest.
/// </summary>
public sealed class CrearUnidadMedidaRequestOptions : ILoadRelatedDataOptions
{
    public bool CargarDatosExtra { get; set; }
}

/// <summary>
///     Respuesta de la solicitud CrearUnidadMedidaRequest.
/// </summary>
public sealed class CrearUnidadMedidaResponse : ContpaqiResponse<CrearUnidadMedidaResponseModel>
{
    public CrearUnidadMedidaResponse(CrearUnidadMedidaResponseModel model) : base(model)
    {
    }

    public static CrearUnidadMedidaResponse CreateInstance(UnidadMedida unidadMedida)
    {
        return new CrearUnidadMedidaResponse(new CrearUnidadMedidaResponseModel(unidadMedida));
    }
}

/// <summary>
///     Modelo de la respuesta CrearUnidadMedidaResponse.
/// </summary>
public sealed class CrearUnidadMedidaResponseModel
{
    public CrearUnidadMedidaResponseModel(UnidadMedida unidadMedida)
    {
        UnidadMedida = unidadMedida;
    }

    public UnidadMedida UnidadMedida { get; set; }
}
