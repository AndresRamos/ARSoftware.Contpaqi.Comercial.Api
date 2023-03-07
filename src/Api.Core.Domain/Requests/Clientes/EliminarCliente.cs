namespace Api.Core.Domain.Requests;

/// <inheritdoc cref="IApiRequest" />
public sealed class EliminarClienteRequest : ApiRequestBase, IApiRequest<EliminarClienteRequestModel, EliminarClienteRequestOptions>
{
    public EliminarClienteRequestModel Model { get; set; } = new();
    public EliminarClienteRequestOptions Options { get; set; } = new();
}

public sealed class EliminarClienteRequestModel
{
    public string CodigoCliente { get; set; } = string.Empty;
}

public sealed class EliminarClienteRequestOptions
{
}

/// <inheritdoc cref="IApiResponse" />
public sealed class EliminarClienteResponse : ApiResponseBase, IApiResponse<EliminarClienteResponseModel>
{
    public EliminarClienteResponseModel Model { get; set; } = new();
}

public sealed class EliminarClienteResponseModel
{
}
