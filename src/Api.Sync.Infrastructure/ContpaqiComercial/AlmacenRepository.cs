﻿using Api.Core.Domain.Common;
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

public sealed class AlmacenRepository : IAlmacenRepository
{
    private readonly ContpaqiComercialEmpresaDbContext _context;
    private readonly IMapper _mapper;

    public AlmacenRepository(ContpaqiComercialEmpresaDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Almacen?> BuscarPorIdAsync(int id, ILoadRelatedDataOptions loadRelatedDataOptions,
        CancellationToken cancellationToken)
    {
        AlmacenDto? almacenSql = await _context.admAlmacenes.Where(m => m.CIDALMACEN == id)
            .ProjectTo<AlmacenDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(cancellationToken);

        if (almacenSql is null) return null;

        var almacen = _mapper.Map<Almacen>(almacenSql);

        await CargarDatosRelacionadosAsync(almacen, almacenSql, loadRelatedDataOptions, cancellationToken);

        return almacen;
    }

    public async Task<Almacen?> BuscarPorCodigoAsync(string codigo, ILoadRelatedDataOptions loadRelatedDataOptions,
        CancellationToken cancellationToken)
    {
        AlmacenDto? almacenSql = await _context.admAlmacenes.Where(m => m.CCODIGOALMACEN == codigo)
            .ProjectTo<AlmacenDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(cancellationToken);

        if (almacenSql is null) return null;

        var almacen = _mapper.Map<Almacen>(almacenSql);

        await CargarDatosRelacionadosAsync(almacen, almacenSql, loadRelatedDataOptions, cancellationToken);

        return almacen;
    }

    public async Task<bool> ExistePorCodigoAsync(string codigo, CancellationToken cancellationToken)
    {
        return await _context.admAlmacenes.AnyAsync(m => m.CCODIGOALMACEN == codigo, cancellationToken);
    }

    public async Task<IEnumerable<Almacen>> BuscarTodoAsync(ILoadRelatedDataOptions loadRelatedDataOptions,
        CancellationToken cancellationToken)
    {
        var almacenesList = new List<Almacen>();

        List<AlmacenDto> almacenesSql = await _context.admAlmacenes.OrderBy(c => c.CNOMBREALMACEN)
            .ProjectTo<AlmacenDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);

        foreach (AlmacenDto? almacenSql in almacenesSql)
        {
            var almacen = _mapper.Map<Almacen>(almacenSql);

            await CargarDatosRelacionadosAsync(almacen, almacenSql, loadRelatedDataOptions, cancellationToken);

            almacenesList.Add(almacen);
        }

        return almacenesList;
    }

    public async Task<IEnumerable<Almacen>> BuscarPorRequestModelAsync(BuscarAlmacenesRequestModel requestModel,
        ILoadRelatedDataOptions loadRelatedDataOptions, CancellationToken cancellationToken)
    {
        var almacenesList = new List<Almacen>();

        IQueryable<admAlmacenes> almacenesQuery = !string.IsNullOrWhiteSpace(requestModel.SqlQuery)
            ? _context.admAlmacenes.FromSqlRaw($"SELECT * FROM admAlmacenes WHERE {requestModel.SqlQuery}")
            : _context.admAlmacenes.AsQueryable();

        if (requestModel.Id is not null) almacenesQuery = almacenesQuery.Where(a => a.CIDALMACEN == requestModel.Id);

        if (!string.IsNullOrWhiteSpace(requestModel.Codigo))
            almacenesQuery = almacenesQuery.Where(a => a.CCODIGOALMACEN == requestModel.Codigo);

        List<AlmacenDto> almacenesSql = await almacenesQuery
            .ProjectTo<AlmacenDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);

        foreach (AlmacenDto? almacenSql in almacenesSql)
        {
            var almacen = _mapper.Map<Almacen>(almacenSql);

            await CargarDatosRelacionadosAsync(almacen, almacenSql, loadRelatedDataOptions, cancellationToken);

            almacenesList.Add(almacen);
        }

        return almacenesList;
    }

    private async Task CargarDatosRelacionadosAsync(Almacen almacen, AlmacenDto almacenSql, ILoadRelatedDataOptions loadRelatedDataOptions,
        CancellationToken cancellationToken)
    {
        if (loadRelatedDataOptions.CargarDatosExtra)
            almacen.DatosExtra = (await _context.admAlmacenes.FirstAsync(m => m.CIDALMACEN == almacenSql.CIDALMACEN, cancellationToken))
                .ToDatosDictionary<admAlmacenes>();
    }
}
