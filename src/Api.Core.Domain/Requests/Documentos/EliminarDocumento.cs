using Api.Core.Domain.Models;

namespace Api.Core.Domain.Requests;

/// <inheritdoc cref="IApiRequest" />
public sealed class EliminarDocumentoRequest : ApiRequestBase, IApiRequest<EliminarDocumentoRequestModel, EliminarDocumentoRequestOptions>
{
    public EliminarDocumentoRequestModel Model { get; set; } = new();
    public EliminarDocumentoRequestOptions Options { get; set; } = new();
}

public sealed class EliminarDocumentoRequestModel
{
    public LlaveDocumento LlaveDocumento { get; set; } = new();
}

public sealed class EliminarDocumentoRequestOptions
{
}

/// <inheritdoc cref="IApiResponse" />
public sealed class EliminarDocumentoResponse : ApiResponseBase, IApiResponse<EliminarDocumentoResponseModel>
{
    public EliminarDocumentoResponseModel Model { get; set; } = new();
}

public sealed class EliminarDocumentoResponseModel
{
}
