using Api.Core.Domain.Requests;

namespace Api.Sync.Core.Application.ContpaqiComercial.Interfaces;

public interface IExistenciasProductoRepository
{
    Task<double> BuscaExistenciasAsync(BuscarExistenciasProductoRequestModel requestModel, CancellationToken cancellationToken);

    Task<double> BuscaExistenciasConCapasAsync(BuscarExistenciasProductoConCapasRequestModel requestModel,
        CancellationToken cancellationToken);

    Task<double> BuscaExistenciasConCaracteristicasAsync(BuscarExistenciasProductoConCaracteristicasRequestModel requestModel,
        CancellationToken cancellationToken);
}