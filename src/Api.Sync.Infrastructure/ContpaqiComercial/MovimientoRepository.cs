﻿using Api.Core.Domain.Common;
using Api.Sync.Core.Application.ContpaqiComercial.Interfaces;
using ARSoftware.Contpaqi.Comercial.Sdk.Abstractions.Dtos;
using ARSoftware.Contpaqi.Comercial.Sdk.Extras.Extensions;
using ARSoftware.Contpaqi.Comercial.Sql.Contexts;
using ARSoftware.Contpaqi.Comercial.Sql.Models.Empresa;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace Api.Sync.Infrastructure.ContpaqiComercial;

public sealed class MovimientoRepository : IMovimientoRepository
{
    private readonly IAlmacenRepository _almacenRepository;
    private readonly ContpaqiComercialEmpresaDbContext _context;
    private readonly IMapper _mapper;
    private readonly IProductoRepository _productoRepository;

    public MovimientoRepository(ContpaqiComercialEmpresaDbContext context, IMapper mapper, IUnidadMedidaRepository unidadMedidaRepository)
    {
        _context = context;
        _mapper = mapper;
        _productoRepository = new ProductoRepository(context, mapper, unidadMedidaRepository);
        _almacenRepository = new AlmacenRepository(context, mapper);
    }

    public async Task<IEnumerable<Movimiento>> BuscarPorDocumentoIdAsync(int documentoId, ILoadRelatedDataOptions loadRelatedDataOptions,
        CancellationToken cancellationToken)
    {
        var movimientosList = new List<Movimiento>();

        List<MovimientoDto> movimientosSql = await _context.admMovimientos.Where(m => m.CIDDOCUMENTO == documentoId)
            .ProjectTo<MovimientoDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);

        foreach (MovimientoDto? movimientoSql in movimientosSql)
        {
            var movimiento = _mapper.Map<Movimiento>(movimientoSql);

            await CargarDatosRelacionadosAsync(movimiento, movimientoSql, loadRelatedDataOptions, cancellationToken);

            movimientosList.Add(movimiento);
        }

        return movimientosList;
    }

    private async Task CargarDatosRelacionadosAsync(Movimiento movimiento, MovimientoDto movimientoSql,
        ILoadRelatedDataOptions loadRelatedDataOptions, CancellationToken cancellationToken)
    {
        movimiento.Producto =
            await _productoRepository.BuscarPorIdAsync(movimientoSql.CIDPRODUCTO, loadRelatedDataOptions, cancellationToken) ??
            new Producto();

        movimiento.Almacen = await _almacenRepository.BuscarPorIdAsync(movimientoSql.CIDALMACEN, loadRelatedDataOptions, cancellationToken);

        if (loadRelatedDataOptions.CargarDatosExtra)
            movimiento.DatosExtra =
                (await _context.admMovimientos.FirstAsync(m => m.CIDMOVIMIENTO == movimientoSql.CIDMOVIMIENTO, cancellationToken))
                .ToDatosDictionary<admMovimientos>();
    }
}
