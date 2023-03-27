﻿using Api.Core.Domain.Common;
using Api.Core.Domain.Models;
using Api.Core.Domain.Requests;
using Api.Sync.Core.Application.ContpaqiComercial.Interfaces;
using Api.Sync.Infrastructure.ContpaqiComercial.Models;
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

    public ProductoRepository(ContpaqiComercialEmpresaDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Producto?> BuscarPorIdAsync(int id,
                                                  ILoadRelatedDataOptions loadRelatedDataOptions,
                                                  CancellationToken cancellationToken)
    {
        ProductoSql? productoSql = await _context.admProductos.Where(m => m.CIDPRODUCTO == id)
            .ProjectTo<ProductoSql>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(cancellationToken);

        if (productoSql is null)
            return null;

        var producto = _mapper.Map<Producto>(productoSql);

        await CargarDatosRelacionadosAsync(producto, productoSql, loadRelatedDataOptions, cancellationToken);

        return producto;
    }

    public async Task<Producto?> BuscarPorCodigoAsync(string codigo,
                                                      ILoadRelatedDataOptions loadRelatedDataOptions,
                                                      CancellationToken cancellationToken)
    {
        ProductoSql? productoSql = await _context.admProductos.Where(m => m.CCODIGOPRODUCTO == codigo)
            .ProjectTo<ProductoSql>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(cancellationToken);

        if (productoSql is null)
            return null;

        var producto = _mapper.Map<Producto>(productoSql);

        await CargarDatosRelacionadosAsync(producto, productoSql, loadRelatedDataOptions, cancellationToken);

        return producto;
    }

    public async Task<IEnumerable<Producto>> BuscarTodoAsync(ILoadRelatedDataOptions loadRelatedDataOptions,
                                                             CancellationToken cancellationToken)
    {
        var productosList = new List<Producto>();

        List<ProductoSql> productosSql = await _context.admProductos.OrderBy(c => c.CNOMBREPRODUCTO)
            .ProjectTo<ProductoSql>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);

        foreach (ProductoSql productoSql in productosSql)
        {
            var producto = _mapper.Map<Producto>(productoSql);

            await CargarDatosRelacionadosAsync(producto, productoSql, loadRelatedDataOptions, cancellationToken);

            productosList.Add(producto);
        }

        return productosList;
    }

    public async Task<IEnumerable<Producto>> BuscarPorRequestModel(BuscarProductosRequestModel requestModel,
                                                                   ILoadRelatedDataOptions loadRelatedDataOptions,
                                                                   CancellationToken cancellationToken)
    {
        var productosList = new List<Producto>();

        IQueryable<admProductos> productosQuery = !string.IsNullOrWhiteSpace(requestModel.SqlQuery)
            ? _context.admProductos.FromSqlRaw($"SELECT * FROM admProductos WHERE {requestModel.SqlQuery}")
            : _context.admProductos.AsQueryable();

        if (requestModel.Id is not null)
            productosQuery = productosQuery.Where(a => a.CIDPRODUCTO == requestModel.Id);

        if (!string.IsNullOrWhiteSpace(requestModel.Codigo))
            productosQuery = productosQuery.Where(a => a.CCODIGOPRODUCTO == requestModel.Codigo);

        List<ProductoSql> productosSql = await productosQuery
            .ProjectTo<ProductoSql>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);

        foreach (ProductoSql productoSql in productosSql)
        {
            var producto = _mapper.Map<Producto>(productoSql);

            await CargarDatosRelacionadosAsync(producto, productoSql, loadRelatedDataOptions, cancellationToken);

            productosList.Add(producto);
        }

        return productosList;
    }

    private async Task CargarDatosRelacionadosAsync(Producto producto,
                                                    ProductoSql productoSql,
                                                    ILoadRelatedDataOptions loadRelatedDataOptions,
                                                    CancellationToken cancellationToken)
    {
        if (loadRelatedDataOptions.CargarDatosExtra)
            producto.DatosExtra = (await _context.admProductos.FirstAsync(m => m.CIDPRODUCTO == productoSql.CIDPRODUCTO, cancellationToken))
                .ToDatosDictionary<admProductos>();
    }
}
