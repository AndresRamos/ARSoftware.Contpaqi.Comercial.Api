namespace Api.Core.Domain.Models;

/// <summary>
///     Estructura de una direccion.
/// </summary>
public sealed class Direccion
{
    /// <summary>
    ///     Nombre de la calle.
    /// </summary>
    public string Calle { get; set; } = string.Empty;

    /// <summary>
    ///     Numero exterior de la calle.
    /// </summary>
    public string NumeroExterior { get; set; } = string.Empty;

    /// <summary>
    ///     Numero interior del edificio o local.
    /// </summary>
    public string NumeroInterior { get; set; } = string.Empty;

    /// <summary>
    ///     Colonia o fraccionamiento.
    /// </summary>
    public string Colonia { get; set; } = string.Empty;

    /// <summary>
    ///     Nombre de la ciudad.
    /// </summary>
    public string Ciudad { get; set; } = string.Empty;

    /// <summary>
    ///     Nombre del estado.
    /// </summary>
    public string Estado { get; set; } = string.Empty;

    /// <summary>
    ///     Codigo postal.
    /// </summary>
    public string CodigoPostal { get; set; } = string.Empty;

    /// <summary>
    ///     Nombre del pais.
    /// </summary>
    public string Pais { get; set; } = string.Empty;
}
