using ARSoftware.Contpaqi.Api.Common.Domain;

namespace Api.Core.Domain.Requests;

/// <summary>
///     Solicitud para buscar unidades de medida.
/// </summary>
public sealed class BuscarUnidadesMedidaRequest : ContpaqiRequest<BuscarUnidadesMedidaRequestModel, BuscarUnidadesMedidaRequestOptions,
    BuscarUnidadesMedidaResponse>
{
    public BuscarUnidadesMedidaRequest(BuscarUnidadesMedidaRequestModel model, BuscarUnidadesMedidaRequestOptions options) : base(model,
        options)
    {
    }
}

/// <summary>
///     Modelo de la solicitud BuscarUnidadesMedidaRequest.
/// </summary>
public sealed class BuscarUnidadesMedidaRequestModel
{
}

/// <summary>
///     Opciones de la solicitud BuscarUnidadesMedidaRequest.
/// </summary>
public sealed class BuscarUnidadesMedidaRequestOptions : ILoadRelatedDataOptions
{
    public bool CargarDatosExtra { get; set; }
}

/// <summary>
///     Respuesta de la solicitud BuscarUnidadesMedidaRequest.
/// </summary>
public sealed class BuscarUnidadesMedidaResponse : ContpaqiResponse<BuscarUnidadesMedidaResponseModel>
{
    public BuscarUnidadesMedidaResponse(BuscarUnidadesMedidaResponseModel model) : base(model)
    {
    }

    public static BuscarUnidadesMedidaResponse CreateInstance(List<UnidadMedida> unidadesMedida)
    {
        return new BuscarUnidadesMedidaResponse(new BuscarUnidadesMedidaResponseModel(unidadesMedida));
    }
}

/// <summary>
///     Modelo de la respuesta BuscarUnidadesMedidaResponse.
/// </summary>
public sealed class BuscarUnidadesMedidaResponseModel
{
    public BuscarUnidadesMedidaResponseModel(List<UnidadMedida> unidadesMedida)
    {
        UnidadesMedida = unidadesMedida;
    }

    public int NumeroRegistros => UnidadesMedida.Count;

    public List<UnidadMedida> UnidadesMedida { get; set; }
}
