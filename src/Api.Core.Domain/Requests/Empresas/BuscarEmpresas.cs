using Api.Core.Domain.Models;
using ARSoftware.Contpaqi.Api.Common.Domain;

namespace Api.Core.Domain.Requests;

/// <summary>
///     Solicitud para buscar empresas.
/// </summary>
public sealed class
    BuscarEmpresasRequest : ContpaqiRequest<BuscarEmpresasRequestModel, BuscarEmpresasRequestOptions, BuscarEmpresasResponse>
{
    public BuscarEmpresasRequest(BuscarEmpresasRequestModel model, BuscarEmpresasRequestOptions options) : base(model, options)
    {
    }
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
public sealed class BuscarEmpresasResponse : ContpaqiResponse<BuscarEmpresasResponseModel>
{
    public BuscarEmpresasResponse(BuscarEmpresasResponseModel model) : base(model)
    {
    }

    public static BuscarEmpresasResponse CreateInstance(List<Empresa> empresas)
    {
        return new BuscarEmpresasResponse(new BuscarEmpresasResponseModel(empresas));
    }
}

/// <summary>
///     Modelo de la solicitud BuscarEmpresasResponse.
/// </summary>
public sealed class BuscarEmpresasResponseModel
{
    public BuscarEmpresasResponseModel(List<Empresa> empresas)
    {
        Empresas = empresas;
    }

    public int NumeroRegistros => Empresas.Count;

    /// <summary>
    ///     Lista de empresas encontradas.
    /// </summary>
    public List<Empresa> Empresas { get; set; }
}
