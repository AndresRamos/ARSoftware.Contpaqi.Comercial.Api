using Api.Core.Domain.Models;

namespace Api.Core.Domain.Requests;

/// <inheritdoc cref="IApiRequest" />
public sealed class BuscarEmpresasRequest : ApiRequestBase, IApiRequest<BuscarEmpresasRequestModel, BuscarEmpresasRequestOptions>
{
    public BuscarEmpresasRequestModel Model { get; set; } = new();
    public BuscarEmpresasRequestOptions Options { get; set; } = new();
}

public sealed class BuscarEmpresasRequestModel
{
}

public sealed class BuscarEmpresasRequestOptions
{
}

/// <inheritdoc cref="IApiResponse" />
public sealed class BuscarEmpresasResponse : ApiResponseBase, IApiResponse<BuscarEmpresasResponseModel>
{
    public BuscarEmpresasResponseModel Model { get; set; } = new();
}

public sealed class BuscarEmpresasResponseModel
{
    public List<Empresa> Empresas { get; set; } = new();
}
