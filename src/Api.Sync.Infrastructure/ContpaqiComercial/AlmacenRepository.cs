using Api.Core.Domain.Models;
using Api.Sync.Core.Application.ContpaqiComercial.Interfaces;
using ARSoftware.Contpaqi.Comercial.Sdk.Extras.Extensions;
using ARSoftware.Contpaqi.Comercial.Sql.Contexts;
using ARSoftware.Contpaqi.Comercial.Sql.Models.Empresa;
using AutoMapper;
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

    public async Task<Almacen?> BuscarPorIdAsync(int id, CancellationToken cancellationToken)
    {
        admAlmacenes? almacenSql = await _context.admAlmacenes.FirstOrDefaultAsync(m => m.CIDALMACEN == id, cancellationToken);

        if (almacenSql is null)
            return null;

        var almacen = _mapper.Map<Almacen>(almacenSql);

        almacen.DatosExtra = almacenSql.ToDatosDictionary<admAlmacenes>();

        return almacen;
    }

    public async Task<Almacen?> BuscarPorCodigoAsync(string codigo, CancellationToken cancellationToken)
    {
        admAlmacenes? almacenSql = await _context.admAlmacenes.FirstOrDefaultAsync(m => m.CCODIGOALMACEN == codigo, cancellationToken);

        if (almacenSql is null)
            return null;

        var almacen = _mapper.Map<Almacen>(almacenSql);

        almacen.DatosExtra = almacenSql.ToDatosDictionary<admAlmacenes>();

        return almacen;
    }

    public async Task<IEnumerable<Almacen>> BuscarTodoAsync(CancellationToken cancellationToken)
    {
        var almacenesList = new List<Almacen>();

        List<admAlmacenes> almacenesSql = await _context.admAlmacenes.OrderBy(c => c.CNOMBREALMACEN).ToListAsync(cancellationToken);

        foreach (admAlmacenes almacenSql in almacenesSql)
        {
            var almacen = _mapper.Map<Almacen>(almacenSql);

            almacen.DatosExtra = almacenSql.ToDatosDictionary<admAgentes>();

            almacenesList.Add(almacen);
        }

        return almacenesList;
    }
}
