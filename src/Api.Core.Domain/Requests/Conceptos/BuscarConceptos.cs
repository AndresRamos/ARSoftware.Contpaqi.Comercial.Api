using Api.Core.Domain.Models;
using ARSoftware.Contpaqi.Api.Common.Domain;

namespace Api.Core.Domain.Requests;

/// <summary>
///     Solicitud para buscar conceptos.
/// </summary>
public sealed class
    BuscarConceptosRequest : ContpaqiRequest<BuscarConceptosRequestModel, BuscarConceptosRequestOptions, BuscarConceptosResponse>
{
    public BuscarConceptosRequest(BuscarConceptosRequestModel model, BuscarConceptosRequestOptions options) : base(model, options)
    {
    }
}

/// <summary>
///     Modelo de la solicitud BuscarConceptosRequest.
/// </summary>
public sealed class BuscarConceptosRequestModel
{
    /// <summary>
    ///     Parametro para buscar conceptos por id.
    /// </summary>
    public int? Id { get; set; }

    /// <summary>
    ///     Parametro para buscar conceptos por codigo.
    /// </summary>
    public string? Codigo { get; set; }

    /// <summary>
    ///     Parametro para buscar conceptos por SQL. El valor debe ser el WHERE clause y debes asegurarte de sanatizar tu SQL.
    ///     <example>
    ///         <para>Ejemplo para buscar conceptos por nombre:</para>
    ///         <code>SqlQuery = "CNOMBRECONCEPTO = 'nombre'"</code>
    ///         <para>se traduce a </para>
    ///         <code>SELECT * FROM admConceptos WHERE CNOMBRECONCEPTO = 'nombre'</code>
    ///     </example>
    /// </summary>
    public string? SqlQuery { get; set; }
}

/// <summary>
///     Opciones de la solicitud BuscarConceptosRequest.
/// </summary>
/// <inheritdoc cref="ILoadRelatedDataOptions" />
public sealed class BuscarConceptosRequestOptions : ILoadRelatedDataOptions
{
    public bool CargarDatosExtra { get; set; }
}

/// <summary>
///     Respuesta de la solicitud BuscarConceptosRequest.
/// </summary>
public sealed class BuscarConceptosResponse : ContpaqiResponse<BuscarConceptosResponseModel>
{
    public BuscarConceptosResponse(BuscarConceptosResponseModel model) : base(model)
    {
    }

    public static BuscarConceptosResponse CreateInstance(List<Concepto> conceptos)
    {
        return new BuscarConceptosResponse(new BuscarConceptosResponseModel(conceptos));
    }
}

/// <summary>
///     Modelo de la respuesta BuscarConceptosRequest.
/// </summary>
public sealed class BuscarConceptosResponseModel
{
    public BuscarConceptosResponseModel(List<Concepto> conceptos)
    {
        Conceptos = conceptos;
    }

    public int NumeroRegistros => Conceptos.Count;

    /// <summary>
    ///     Lista de conceptos encontrados.
    /// </summary>
    public List<Concepto> Conceptos { get; set; }
}
