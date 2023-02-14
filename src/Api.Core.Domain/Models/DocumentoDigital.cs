namespace Api.Core.Domain.Models;

public sealed class DocumentoDigital
{
    /// <summary>
    ///     Nombre del documento.
    /// </summary>
    public string Nombre { get; set; } = string.Empty;

    /// <summary>
    ///     Tipo de documento.
    /// </summary>
    public string Tipo { get; set; } = string.Empty;

    /// <summary>
    ///     Ubicacion del documento.
    /// </summary>
    public string Ubicacion { get; set; } = string.Empty;
}
