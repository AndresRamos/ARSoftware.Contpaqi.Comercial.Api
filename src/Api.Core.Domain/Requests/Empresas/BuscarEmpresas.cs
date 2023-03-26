using Api.Core.Domain.Models;

namespace Api.Core.Domain.Requests;

/// <summary>
///     Solicitud para buscar empresas.
/// </summary>
public sealed class BuscarEmpresasRequest : ApiRequestBase, IApiRequest<BuscarEmpresasRequestModel, BuscarEmpresasRequestOptions>
{
    public BuscarEmpresasRequestModel Model { get; set; } = new();
    public BuscarEmpresasRequestOptions Options { get; set; } = new();
}

/// <summary>
///     Modelo de la solicitud BuscarEmpresasRequest.
/// </summary>
public sealed class BuscarEmpresasRequestModel
{
}

/// <summary>
///     Opciones de la solicitud BuscarEmpresasRequest.
/// </summary>
/// <inheritdoc cref="ILoadRelatedDataOptions" />
public sealed class BuscarEmpresasRequestOptions : ILoadRelatedDataOptions
{
    public bool CargarDatosExtra { get; set; }
}

/// <summary>
///     Respuesta de la solicitud BuscarEmpresasRequest.
/// </summary>
public sealed class BuscarEmpresasResponse : ApiResponseBase, IApiResponse<BuscarEmpresasResponseModel>
{
    public BuscarEmpresasResponseModel Model { get; set; } = new();
}

/// <summary>
///     Modelo de la solicitud BuscarEmpresasResponse.
/// </summary>
public sealed class BuscarEmpresasResponseModel
{
    /// <summary>
    ///     Lista de empresas encontradas.
    /// </summary>
    public List<Empresa> Empresas { get; set; } = new();
}
