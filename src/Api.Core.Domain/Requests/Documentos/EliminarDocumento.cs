using ARSoftware.Contpaqi.Api.Common.Domain;

namespace Api.Core.Domain.Requests;

/// <summary>
///     Solicitud para eliminar un documento.
/// </summary>
public sealed class EliminarDocumentoRequest :
    ContpaqiRequest<EliminarDocumentoRequestModel, EliminarDocumentoRequestOptions, EliminarDocumentoResponse>
{
    public EliminarDocumentoRequest(EliminarDocumentoRequestModel model, EliminarDocumentoRequestOptions options) : base(model, options)
    {
    }
}

/// <summary>
///     Modelo de la solicitud EliminarDocumentoRequest.
/// </summary>
public sealed class EliminarDocumentoRequestModel
{
    public LlaveDocumento LlaveDocumento { get; set; } = new();
}

/// <summary>
///     Opciones de la solicitud EliminarDocumentoRequest.
/// </summary>
public sealed class EliminarDocumentoRequestOptions
{
}

/// <summary>
///     Respuesta de la solicitud EliminarDocumentoRequest.
/// </summary>
public sealed class EliminarDocumentoResponse : ContpaqiResponse<EliminarDocumentoResponseModel>
{
    public EliminarDocumentoResponse(EliminarDocumentoResponseModel model) : base(model)
    {
    }

    public static EliminarDocumentoResponse CreateInstance()
    {
        return new EliminarDocumentoResponse(new EliminarDocumentoResponseModel());
    }
}

/// <summary>
///     Modelo de la respuesta EliminarDocumentoResponse.
/// </summary>
public sealed class EliminarDocumentoResponseModel
{
}
