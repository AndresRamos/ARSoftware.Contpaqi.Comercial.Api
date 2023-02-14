namespace Api.Core.Domain.Models;

/// <summary>
///     Estructura de un agente.
/// </summary>
public sealed class Agente
{
    /// <summary>
    ///     Codigo del agente.
    /// </summary>
    public string Codigo { get; set; } = string.Empty;

    /// <summary>
    ///     Nombre del agente.
    /// </summary>
    public string Nombre { get; set; } = string.Empty;
}
