using Api.Core.Domain.Common;
using Api.Core.Domain.Requests;
using Api.Sync.Core.Application.ContpaqiComercial.Interfaces;
using ARSoftware.Contpaqi.Comercial.Sdk.Abstractions.Dtos;
using ARSoftware.Contpaqi.Comercial.Sdk.Extras.Extensions;
using ARSoftware.Contpaqi.Comercial.Sql.Contexts;
using ARSoftware.Contpaqi.Comercial.Sql.Models.Empresa;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace Api.Sync.Infrastructure.ContpaqiComercial;

public sealed class ProductoRepository : IProductoRepository
{
    private readonly ContpaqiComercialEmpresaDbContext _context;
    private readonly IMapper _mapper;
    private readonly IUnidadMedidaRepository _unidadMedidaRepository;

    public ProductoRepository(ContpaqiComercialEmpresaDbContext context, IMapper mapper, IUnidadMedidaRepository unidadMedidaRepository)
    {
        _context = context;
        _mapper = mapper;
        _unidadMedidaRepository = unidadMedidaRepository;
    }

    public async Task<Producto?> BuscarPorIdAsync(int id, ILoadRelatedDataOptions loadRelatedDataOptions,
        CancellationToken cancellationToken)
    {
        ProductoDto? productoSql = await _context.admProductos.Where(m => m.CIDPRODUCTO == id)
            .ProjectTo<ProductoDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(cancellationToken);

        if (productoSql is null) return null;

        var producto = _mapper.Map<Producto>(productoSql);

        await CargarDatosRelacionadosAsync(producto, productoSql, loadRelatedDataOptions, cancellationToken);

        return producto;
    }

    public async Task<Producto?> BuscarPorCodigoAsync(string codigo, ILoadRelatedDataOptions loadRelatedDataOptions,
        CancellationToken cancellationToken)
    {
        ProductoDto? productoSql = await _context.admProductos.Where(m => m.CCODIGOPRODUCTO == codigo)
            .ProjectTo<ProductoDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(cancellationToken);

        if (productoSql is null) return null;

        var producto = _mapper.Map<Producto>(productoSql);

        await CargarDatosRelacionadosAsync(producto, productoSql, loadRelatedDataOptions, cancellationToken);

        return producto;
    }

    public async Task<bool> ExistePorCodigoAsync(string codigo, CancellationToken cancellationToken)
    {
        return await _context.admProductos.AnyAsync(m => m.CCODIGOPRODUCTO == codigo, cancellationToken);
    }

    public async Task<IEnumerable<Producto>> BuscarPorRequestModel(BuscarProductosRequestModel requestModel,
        ILoadRelatedDataOptions loadRelatedDataOptions, CancellationToken cancellationToken)
    {
        var productosList = new List<Producto>();

        IQueryable<admProductos> productosQuery = !string.IsNullOrWhiteSpace(requestModel.SqlQuery)
            ? _context.admProductos.FromSqlRaw($"SELECT * FROM admProductos WHERE {requestModel.SqlQuery}")
            : _context.admProductos.AsQueryable();

        if (requestModel.Id is not null) productosQuery = productosQuery.Where(a => a.CIDPRODUCTO == requestModel.Id);

        if (!string.IsNullOrWhiteSpace(requestModel.Codigo))
            productosQuery = productosQuery.Where(a => a.CCODIGOPRODUCTO == requestModel.Codigo);

        List<ProductoDto> productosSql = await productosQuery
            .ProjectTo<ProductoDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);

        foreach (ProductoDto productoSql in productosSql)
        {
            var producto = _mapper.Map<Producto>(productoSql);

            await CargarDatosRelacionadosAsync(producto, productoSql, loadRelatedDataOptions, cancellationToken);

            productosList.Add(producto);
        }

        return productosList;
    }

    private async Task CargarDatosRelacionadosAsync(Producto producto, ProductoDto productoSql,
        ILoadRelatedDataOptions loadRelatedDataOptions, CancellationToken cancellationToken)
    {
        producto.UnidadMedida = _unidadMedidaRepository.BuscarPorId(productoSql.CIDUNIDADBASE) ?? new UnidadMedida();

        if (loadRelatedDataOptions.CargarDatosExtra)
            producto.DatosExtra = (await _context.admProductos.FirstAsync(m => m.CIDPRODUCTO == productoSql.CIDPRODUCTO, cancellationToken))
                .ToDatosDictionary<admProductos>();
    }
}
