using Api.Core.Domain.Models;
using Api.Sync.Core.Application.ContpaqiComercial.Interfaces;
using ARSoftware.Contpaqi.Comercial.Sdk.Extras.Extensions;
using ARSoftware.Contpaqi.Comercial.Sql.Contexts;
using ARSoftware.Contpaqi.Comercial.Sql.Models.Empresa;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace Api.Sync.Infrastructure.ContpaqiComercial;

public sealed class MovimientoRepository : IMovimientoRepository
{
    private readonly IAlmacenRepository _almacenRepository;
    private readonly ContpaqiComercialEmpresaDbContext _context;
    private readonly IMapper _mapper;
    private readonly IProductoRepository _productoRepository;

    public MovimientoRepository(ContpaqiComercialEmpresaDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
        _productoRepository = new ProductoRepository(context, mapper);
        _almacenRepository = new AlmacenRepository(context, mapper);
    }

    public async Task<IEnumerable<Movimiento>> BuscarPorDocumentoIdAsync(int documentoId, CancellationToken cancellationToken)
    {
        var movimientosList = new List<Movimiento>();

        List<admMovimientos> movimientosSql = await _context.admMovimientos
            .Where(m => m.CIDDOCUMENTO == documentoId)
            .ToListAsync(cancellationToken);

        foreach (admMovimientos movimientoSql in movimientosSql)
        {
            var movimiento = _mapper.Map<Movimiento>(movimientoSql);

            await CargarObjectosRelacionadosAsync(movimiento, movimientoSql, cancellationToken);

            movimientosList.Add(movimiento);
        }

        return movimientosList;
    }

    private async Task CargarObjectosRelacionadosAsync(Movimiento movimiento,
                                                       admMovimientos movimientoSql,
                                                       CancellationToken cancellationToken)
    {
        movimiento.Producto = await _productoRepository.BuscarPorIdAsync(movimientoSql.CIDPRODUCTO, cancellationToken) ?? new Producto();
        movimiento.Almacen = await _almacenRepository.BuscarPorIdAsync(movimientoSql.CIDALMACEN, cancellationToken) ?? new Almacen();
        movimiento.DatosExtra = movimientoSql.ToDatosDictionary<admMovimientos>();
    }
}
