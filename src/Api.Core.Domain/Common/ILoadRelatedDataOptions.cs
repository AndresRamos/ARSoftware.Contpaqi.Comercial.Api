namespace Api.Core.Domain.Common;

public interface ILoadRelatedDataOptions
{
    /// <summary>
    ///     Opcion para gargar los datos extra de los objectos.
    /// </summary>
    bool CargarDatosExtra { get; set; }
}
