using Api.Core.Domain.Models;
using ARSoftware.Contpaqi.Api.Common.Domain;

namespace Api.Core.Domain.Requests;

/// <summary>
///     Solicitud para buscar agentes.
/// </summary>
public sealed class BuscarAgentesRequest : ContpaqiRequest<BuscarAgentesRequestModel, BuscarAgentesRequestOptions, BuscarAgentesResponse>
{
    public BuscarAgentesRequest(BuscarAgentesRequestModel model, BuscarAgentesRequestOptions options) : base(model, options)
    {
    }
}

/// <summary>
///     Modelo de la solicitud BuscarAgentesRequest.
/// </summary>
public sealed class BuscarAgentesRequestModel
{
    /// <summary>
    ///     Parametro para buscar agentes por id.
    /// </summary>
    public int? Id { get; set; }

    /// <summary>
    ///     Parametro para buscar agentes por codigo.
    /// </summary>
    public string? Codigo { get; set; }

    /// <summary>
    ///     Parametro para buscar agentes por SQL. El valor debe ser el WHERE clause y debes asegurarte de sanatizar tu SQL.
    ///     <example>
    ///         <para>Ejemplo para buscar agentes de tipo Agente de Ventas:</para>
    ///         <code>SqlQuery = "CTIPOAGENTE = 1"</code>
    ///         <para>se traduce a </para>
    ///         <code>SELECT * FROM admAgentes WHERE CTIPOAGENTE = 1</code>
    ///     </example>
    /// </summary>
    public string? SqlQuery { get; set; }
}

/// <summary>
///     Opciones de la solicitud BuscarAgentesRequest.
/// </summary>
/// <inheritdoc cref="ILoadRelatedDataOptions" />
public sealed class BuscarAgentesRequestOptions : ILoadRelatedDataOptions
{
    public bool CargarDatosExtra { get; set; }
}

/// <summary>
///     Respuesta de la solicitud BuscarAgentesRequest.
/// </summary>
public sealed class BuscarAgentesResponse : ContpaqiResponse<BuscarAgentesResponseModel>
{
    public BuscarAgentesResponse(BuscarAgentesResponseModel model) : base(model)
    {
    }

    public static BuscarAgentesResponse CreateInstance(List<Agente> agentes)
    {
        return new BuscarAgentesResponse(new BuscarAgentesResponseModel(agentes));
    }
}

/// <summary>
///     Modelo de la respuesta BuscarAgentesResponse.
/// </summary>
public sealed class BuscarAgentesResponseModel
{
    public BuscarAgentesResponseModel(List<Agente> agentes)
    {
        Agentes = agentes;
    }

    public int NumeroRegistros => Agentes.Count;
    public List<Agente> Agentes { get; set; }
}
