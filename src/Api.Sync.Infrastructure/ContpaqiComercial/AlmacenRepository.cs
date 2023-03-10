using Api.Core.Domain.Common;
using Api.Core.Domain.Models;
using Api.Sync.Core.Application.ContpaqiComercial.Interfaces;
using Api.Sync.Infrastructure.ContpaqiComercial.Models;
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

    public async Task<Almacen?> BuscarPorIdAsync(int id,
                                                 ILoadRelatedDataOptions loadRelatedDataOptions,
                                                 CancellationToken cancellationToken)
    {
        AlmacenSql? almacenSql = await _context.admAlmacenes.Where(m => m.CIDALMACEN == id)
            .ProjectTo<AlmacenSql>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(cancellationToken);

        if (almacenSql is null)
            return null;

        var almacen = _mapper.Map<Almacen>(almacenSql);

        await CargarDatosRelacionadosAsync(almacen, almacenSql, loadRelatedDataOptions, cancellationToken);

        return almacen;
    }

    public async Task<Almacen?> BuscarPorCodigoAsync(string codigo,
                                                     ILoadRelatedDataOptions loadRelatedDataOptions,
                                                     CancellationToken cancellationToken)
    {
        AlmacenSql? almacenSql = await _context.admAlmacenes.Where(m => m.CCODIGOALMACEN == codigo)
            .ProjectTo<AlmacenSql>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(cancellationToken);

        if (almacenSql is null)
            return null;

        var almacen = _mapper.Map<Almacen>(almacenSql);

        await CargarDatosRelacionadosAsync(almacen, almacenSql, loadRelatedDataOptions, cancellationToken);

        return almacen;
    }

    public async Task<IEnumerable<Almacen>> BuscarTodoAsync(ILoadRelatedDataOptions loadRelatedDataOptions,
                                                            CancellationToken cancellationToken)
    {
        var almacenesList = new List<Almacen>();

        List<AlmacenSql> almacenesSql = await _context.admAlmacenes.OrderBy(c => c.CNOMBREALMACEN)
            .ProjectTo<AlmacenSql>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);

        foreach (AlmacenSql? almacenSql in almacenesSql)
        {
            var almacen = _mapper.Map<Almacen>(almacenSql);

            await CargarDatosRelacionadosAsync(almacen, almacenSql, loadRelatedDataOptions, cancellationToken);

            almacenesList.Add(almacen);
        }

        return almacenesList;
    }

    private async Task CargarDatosRelacionadosAsync(Almacen almacen,
                                                    AlmacenSql almacenSql,
                                                    ILoadRelatedDataOptions loadRelatedDataOptions,
                                                    CancellationToken cancellationToken)
    {
        if (loadRelatedDataOptions.CargarDatosExtra)
            almacen.DatosExtra = (await _context.admAlmacenes.FirstAsync(m => m.CIDALMACEN == almacenSql.CIDALMACEN, cancellationToken))
                .ToDatosDictionary<admAlmacenes>();
    }
}
