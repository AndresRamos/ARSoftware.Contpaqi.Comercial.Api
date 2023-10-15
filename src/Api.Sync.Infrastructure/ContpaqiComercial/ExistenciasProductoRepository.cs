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

    public async Task<double> BuscaExistenciasAsync(string codigoProducto, string codigoAlmacen, DateOnly fecha,
        CancellationToken cancellationToken)
    {
        return await _existenciasSqlRepository.BuscaExistenciasAsync(codigoProducto, codigoAlmacen, fecha, cancellationToken);
    }

    public async Task<double> BuscaExistenciasConCaracteristicasAsync(string codigoProducto, string codigoAlmacen, DateOnly fecha,
        string abreviaturaValorCaracteristica1, string abreviaturaValorCaracteristica2, string abreviaturaValorCaracteristica3,
        CancellationToken cancellationToken)
    {
        return await _existenciasSqlRepository.BuscaExistenciasConCaracteristicasAsync(codigoProducto, codigoAlmacen, fecha,
            abreviaturaValorCaracteristica1, abreviaturaValorCaracteristica2, abreviaturaValorCaracteristica3, cancellationToken);
    }

    public async Task<double> BuscaExistenciasConCapasAsync(string codigoProducto, string codigoAlmacen, string pedimento, string lote,
        CancellationToken cancellationToken)
    {
        return await _existenciasSqlRepository.BuscaExistenciasConCapasAsync(codigoProducto, codigoAlmacen, pedimento, lote,
            cancellationToken);
    }
}