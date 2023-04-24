namespace Api.Core.Domain.Models;

/// <summary>
///     Estructura de una llave de documento.
/// </summary>
public sealed class LlaveDocumento
{
    /// <summary>
    ///     Codigo del concepto del documento.
    /// </summary>
    public string ConceptoCodigo { get; set; } = string.Empty;

    /// <summary>
    ///     Serie del documento.
    /// </summary>
    public string Serie { get; set; } = string.Empty;

    /// <summary>
    ///     Folio del documento.
    /// </summary>
    public int Folio { get; set; }

    public override string ToString()
    {
        return $"Concepto = {ConceptoCodigo}, Serie = {Serie}, Folio = {Folio}";
    }
}
