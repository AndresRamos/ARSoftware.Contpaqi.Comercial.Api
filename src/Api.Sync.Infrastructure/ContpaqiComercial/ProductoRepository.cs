using Api.Core.Domain.Models;
using Api.Sync.Core.Application.ContpaqiComercial.Interfaces;
using ARSoftware.Contpaqi.Comercial.Sdk.Extras.Extensions;
using ARSoftware.Contpaqi.Comercial.Sql.Contexts;
using ARSoftware.Contpaqi.Comercial.Sql.Models.Empresa;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace Api.Sync.Infrastructure.ContpaqiComercial;

public sealed class ProductoRepository : IProductoRepository
{
    private readonly ContpaqiComercialEmpresaDbContext _context;
    private readonly IMapper _mapper;

    public ProductoRepository(ContpaqiComercialEmpresaDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Producto?> BuscarPorIdAsync(int id, CancellationToken cancellationToken)
    {
        admProductos? productoSql = await _context.admProductos.FirstOrDefaultAsync(m => m.CIDPRODUCTO == id, cancellationToken);

        if (productoSql is null)
            return null;

        var producto = _mapper.Map<Producto>(productoSql);

        BuscarObjectosRelacionados(producto, productoSql);

        return producto;
    }

    public async Task<Producto?> BuscarPorCodigoAsync(string codigo, CancellationToken cancellationToken)
    {
        admProductos? productoSql = await _context.admProductos.FirstOrDefaultAsync(m => m.CCODIGOPRODUCTO == codigo, cancellationToken);

        if (productoSql is null)
            return null;

        var producto = _mapper.Map<Producto>(productoSql);

        BuscarObjectosRelacionados(producto, productoSql);

        return producto;
    }

    public async Task<IEnumerable<Producto>> BuscarTodoAsync(CancellationToken cancellationToken)
    {
        var productosList = new List<Producto>();

        List<admProductos> productosSql = await _context.admProductos.OrderBy(c => c.CNOMBREPRODUCTO).ToListAsync(cancellationToken);

        foreach (admProductos productoSql in productosSql)
        {
            var producto = _mapper.Map<Producto>(productoSql);

            BuscarObjectosRelacionados(producto, productoSql);

            productosList.Add(producto);
        }

        return productosList;
    }

    private void BuscarObjectosRelacionados(Producto producto, admProductos productoSql)
    {
        producto.DatosExtra = productoSql.ToDatosDictionary<admProductos>();
    }
}
