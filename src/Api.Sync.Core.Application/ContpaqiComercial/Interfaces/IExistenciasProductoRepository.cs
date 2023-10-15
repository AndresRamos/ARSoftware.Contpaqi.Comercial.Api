namespace Api.Sync.Core.Application.ContpaqiComercial.Interfaces;

public interface IExistenciasProductoRepository
{
    Task<double> BuscaExistenciasAsync(string codigoProducto, string codigoAlmacen, DateOnly fecha, CancellationToken cancellationToken);

    Task<double> BuscaExistenciasConCaracteristicasAsync(string codigoProducto, string codigoAlmacen, DateOnly fecha,
        string abreviaturaValorCaracteristica1, string abreviaturaValorCaracteristica2, string abreviaturaValorCaracteristica3,
        CancellationToken cancellationToken);

    Task<double> BuscaExistenciasConCapasAsync(string codigoProducto, string codigoAlmacen, string pedimento, string lote,
        CancellationToken cancellationToken);
}