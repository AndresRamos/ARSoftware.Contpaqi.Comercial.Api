namespace Api.Core.Domain.Models;

/// <summary>
///     Estructura de un concepto de documento
/// </summary>
public sealed class Concepto
{
    /// <summary>
    ///     Codigo del concepto.
    /// </summary>
    public string Codigo { get; set; } = string.Empty;

    /// <summary>
    ///     Nombre del concepto.
    /// </summary>
    public string Nombre { get; set; } = string.Empty;
}
