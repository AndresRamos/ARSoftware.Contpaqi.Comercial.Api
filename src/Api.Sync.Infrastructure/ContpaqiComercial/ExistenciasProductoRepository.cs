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

    public async Task<double> BuscaExistenciasAsync(BuscarExistenciasProductoRequest request, CancellationToken cancellationToken)
    {
        return await _existenciasSqlRepository.BuscaExistenciasAsync(request.Model.CodigoProducto, request.Model.CodigoAlmacen,
            DateOnly.FromDateTime(request.Model.Fecha), cancellationToken);
    }

    public async Task<double> BuscaExistenciasConCapasAsync(string codigoProducto, string codigoAlmacen, string pedimento, string lote,
        CancellationToken cancellationToken)
    {
        return await _existenciasSqlRepository.BuscaExistenciasConCapasAsync(codigoProducto, codigoAlmacen, pedimento, lote,
            cancellationToken);
    }

    public async Task<double> BuscaExistenciasConCaracteristicasAsync(BuscarExistenciasProductoConCaracteristicasRequest request,
        CancellationToken cancellationToken)
    {
        return await _existenciasSqlRepository.BuscaExistenciasConCaracteristicasAsync(request.Model.CodigoProducto,
            request.Model.CodigoAlmacen, DateOnly.FromDateTime(request.Model.Fecha), request.Model.AbreviaturaValorCaracteristica1,
            request.Model.AbreviaturaValorCaracteristica2, request.Model.AbreviaturaValorCaracteristica3, cancellationToken);
    }
}