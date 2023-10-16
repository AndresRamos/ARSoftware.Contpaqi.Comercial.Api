using Api.Core.Domain.Requests;
using Api.Sync.Core.Application.ContpaqiComercial.Interfaces;
using ARSoftware.Contpaqi.Comercial.Sql.Interfaces;

namespace Api.Sync.Infrastructure.ContpaqiComercial;

public sealed class ExistenciasProductoRepository : IExistenciasProductoRepository
{
    private readonly IExistenciasSqlRepository _existenciasSqlRepository;

    public ExistenciasProductoRepository(IExistenciasSqlRepository existenciasSqlRepository)
    {
        _existenciasSqlRepository = existenciasSqlRepository;
    }

    public async Task<double> BuscaExistenciasAsync(BuscarExistenciasProductoRequestModel requestModel, CancellationToken cancellationToken)
    {
        return await _existenciasSqlRepository.BuscaExistenciasAsync(requestModel.CodigoProducto, requestModel.CodigoAlmacen,
            DateOnly.FromDateTime(requestModel.Fecha), cancellationToken);
    }

    public async Task<double> BuscaExistenciasConCapasAsync(BuscarExistenciasProductoConCapasRequestModel requestModel,
        CancellationToken cancellationToken)
    {
        return await _existenciasSqlRepository.BuscaExistenciasConCapasAsync(requestModel.CodigoProducto, requestModel.CodigoAlmacen,
            requestModel.Pedimento, requestModel.Lote, cancellationToken);
    }

    public async Task<double> BuscaExistenciasConCaracteristicasAsync(BuscarExistenciasProductoConCaracteristicasRequestModel requestModel,
        CancellationToken cancellationToken)
    {
        return await _existenciasSqlRepository.BuscaExistenciasConCaracteristicasAsync(requestModel.CodigoProducto,
            requestModel.CodigoAlmacen, DateOnly.FromDateTime(requestModel.Fecha), requestModel.AbreviaturaValorCaracteristica1,
            requestModel.AbreviaturaValorCaracteristica2, requestModel.AbreviaturaValorCaracteristica3, cancellationToken);
    }
}