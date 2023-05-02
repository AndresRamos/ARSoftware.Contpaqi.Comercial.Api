using Api.Core.Domain.Common;
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

public sealed class ConceptoRepository : IConceptoRepository
{
    private readonly ContpaqiComercialEmpresaDbContext _context;
    private readonly IMapper _mapper;

    public ConceptoRepository(ContpaqiComercialEmpresaDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Concepto?> BuscarPorIdAsync(int id, ILoadRelatedDataOptions loadRelatedDataOptions,
        CancellationToken cancellationToken)
    {
        ConceptoSql? conceptoSql = await _context.admConceptos.Where(m => m.CIDCONCEPTODOCUMENTO == id)
            .ProjectTo<ConceptoSql>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(cancellationToken);

        if (conceptoSql is null)
            return null;

        var concepto = _mapper.Map<Concepto>(conceptoSql);

        await CargarDatosRelacionadosAsync(concepto, conceptoSql, loadRelatedDataOptions, cancellationToken);

        return concepto;
    }

    public async Task<Concepto?> BuscarPorCodigoAsync(string codigo, ILoadRelatedDataOptions loadRelatedDataOptions,
        CancellationToken cancellationToken)
    {
        ConceptoSql? conceptoSql = await _context.admConceptos.Where(m => m.CCODIGOCONCEPTO == codigo)
            .ProjectTo<ConceptoSql>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(cancellationToken);

        if (conceptoSql is null)
            return null;

        var concepto = _mapper.Map<Concepto>(conceptoSql);

        await CargarDatosRelacionadosAsync(concepto, conceptoSql, loadRelatedDataOptions, cancellationToken);

        return concepto;
    }

    public async Task<IEnumerable<Concepto>> BuscarTodoAsync(ILoadRelatedDataOptions loadRelatedDataOptions,
        CancellationToken cancellationToken)
    {
        var conceptosList = new List<Concepto>();

        List<ConceptoSql> coneptosSql = await _context.admConceptos.OrderBy(c => c.CNOMBRECONCEPTO)
            .ProjectTo<ConceptoSql>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);

        foreach (ConceptoSql conceptoSql in coneptosSql)
        {
            var concepto = _mapper.Map<Concepto>(conceptoSql);

            await CargarDatosRelacionadosAsync(concepto, conceptoSql, loadRelatedDataOptions, cancellationToken);

            conceptosList.Add(concepto);
        }

        return conceptosList;
    }

    public async Task<IEnumerable<Concepto>> BuscarPorRequstModelAsync(BuscarConceptosRequestModel requestModel,
        ILoadRelatedDataOptions loadRelatedDataOptions, CancellationToken cancellationToken)
    {
        var conceptosList = new List<Concepto>();

        IQueryable<admConceptos> conceptosQuery = !string.IsNullOrWhiteSpace(requestModel.SqlQuery)
            ? _context.admConceptos.FromSqlRaw($"SELECT * FROM admConceptos WHERE {requestModel.SqlQuery}")
            : _context.admConceptos.AsQueryable();

        if (requestModel.Id is not null)
            conceptosQuery = conceptosQuery.Where(a => a.CIDCONCEPTODOCUMENTO == requestModel.Id);

        if (!string.IsNullOrWhiteSpace(requestModel.Codigo))
            conceptosQuery = conceptosQuery.Where(a => a.CCODIGOCONCEPTO == requestModel.Codigo);

        List<ConceptoSql> coneptosSql = await conceptosQuery
            .ProjectTo<ConceptoSql>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);

        foreach (ConceptoSql conceptoSql in coneptosSql)
        {
            var concepto = _mapper.Map<Concepto>(conceptoSql);

            await CargarDatosRelacionadosAsync(concepto, conceptoSql, loadRelatedDataOptions, cancellationToken);

            conceptosList.Add(concepto);
        }

        return conceptosList;
    }

    private async Task CargarDatosRelacionadosAsync(Concepto concepto, ConceptoSql conceptoSql,
        ILoadRelatedDataOptions loadRelatedDataOptions, CancellationToken cancellationToken)
    {
        if (loadRelatedDataOptions.CargarDatosExtra)
            concepto.DatosExtra =
                (await _context.admConceptos.FirstAsync(c => c.CIDCONCEPTODOCUMENTO == conceptoSql.CIDCONCEPTODOCUMENTO, cancellationToken))
                .ToDatosDictionary<admConceptos>();
    }
}
