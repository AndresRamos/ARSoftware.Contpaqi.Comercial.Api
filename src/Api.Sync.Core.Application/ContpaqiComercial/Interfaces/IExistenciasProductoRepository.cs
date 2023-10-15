using Api.Core.Domain.Requests;

namespace Api.Sync.Core.Application.ContpaqiComercial.Interfaces;

public interface IExistenciasProductoRepository
{
    Task<double> BuscaExistenciasAsync(BuscarExistenciasProductoRequest request, CancellationToken cancellationToken);

    Task<double> BuscaExistenciasConCaracteristicasAsync(BuscarExistenciasProductoConCaracteristicasRequest request,
        CancellationToken cancellationToken);

    Task<double> BuscaExistenciasConCapasAsync(string codigoProducto, string codigoAlmacen, string pedimento, string lote,
        CancellationToken cancellationToken);
}