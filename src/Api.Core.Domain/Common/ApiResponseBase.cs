namespace Api.Core.Domain.Common;

public abstract class ApiResponseBase
{
    /// <summary>
    ///     Id de la respuesta.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    ///     Fecha de creacion de la respuesta.
    /// </summary>
    public DateTime DateCreated { get; set; } = DateTime.Today;

    /// <summary>
    ///     Mensaje de error.
    /// </summary>
    public string ErrorMessage { get; set; } = string.Empty;

    /// <summary>
    ///     Resultado de si fue exitosa la solicitud.
    /// </summary>
    public bool IsSuccess { get; set; }

    /// <summary>
    ///     Tiempo de ejecucion en milisegundos.
    /// </summary>
    public long ExecutionTime { get; set; }
}
