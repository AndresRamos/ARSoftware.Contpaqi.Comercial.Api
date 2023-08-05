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
    ///     Ubicación del documento.
    /// </summary>
    public string Ubicacion { get; set; } = string.Empty;

    /// <summary>
    ///     Contenido del documento. Cuando se serializa a JSON, se convierte a Base64.
    /// </summary>
    public byte[]? Contenido { get; set; }
}
