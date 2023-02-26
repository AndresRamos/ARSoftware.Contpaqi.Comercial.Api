namespace Api.Core.Domain.Requests;

/// <inheritdoc cref="IApiRequest" />
public sealed class EliminarProductoRequest : ApiRequestBase, IApiRequest<EliminarProductoRequestModel, EliminarProductoRequestOptions>
{
    public EliminarProductoRequestModel Model { get; set; } = new();
    public EliminarProductoRequestOptions Options { get; set; } = new();
}

public sealed class EliminarProductoRequestModel
{
    public string CodigoProducto { get; set; } = string.Empty;
}

public sealed class EliminarProductoRequestOptions
{
}

/// <inheritdoc cref="IApiResponse" />
public sealed class EliminarProductoResponse : ApiResponseBase, IApiResponse<EliminarProductoResponseModel>
{
    public EliminarProductoResponseModel Model { get; set; } = new();
}

public sealed class EliminarProductoResponseModel
{
}
