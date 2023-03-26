namespace Api.Core.Domain.Common;

public interface ILoadRelatedDataOptions
{
    /// <summary>
    ///     Opcion para cargar los datos extra de los objectos.
    /// </summary>
    bool CargarDatosExtra { get; set; }
}
