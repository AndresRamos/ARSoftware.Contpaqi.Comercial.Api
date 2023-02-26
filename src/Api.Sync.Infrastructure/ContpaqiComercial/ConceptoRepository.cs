using Api.Core.Domain.Models;
using Api.Sync.Core.Application.ContpaqiComercial.Interfaces;
using ARSoftware.Contpaqi.Comercial.Sdk.Extras.Extensions;
using ARSoftware.Contpaqi.Comercial.Sql.Contexts;
using ARSoftware.Contpaqi.Comercial.Sql.Models.Empresa;
using AutoMapper;
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

    public async Task<Concepto?> BuscarPorIdAsync(int id, CancellationToken cancellationToken)
    {
        admConceptos? conceptoSql = await _context.admConceptos.FirstOrDefaultAsync(m => m.CIDCONCEPTODOCUMENTO == id, cancellationToken);

        if (conceptoSql is null)
            return null;

        var agente = _mapper.Map<Concepto>(conceptoSql);

        BuscarObjectosRelacionados(agente, conceptoSql);

        return agente;
    }

    public async Task<Concepto?> BuscarPorCodigoAsync(string codigo, CancellationToken cancellationToken)
    {
        admConceptos? conceptoSql = await _context.admConceptos.FirstOrDefaultAsync(m => m.CCODIGOCONCEPTO == codigo, cancellationToken);

        if (conceptoSql is null)
            return null;

        var agente = _mapper.Map<Concepto>(conceptoSql);

        BuscarObjectosRelacionados(agente, conceptoSql);

        return agente;
    }

    public async Task<IEnumerable<Concepto>> BuscarTodoAsync(CancellationToken cancellationToken)
    {
        var conceptosList = new List<Concepto>();

        List<admConceptos> coneptosSql = await _context.admConceptos.OrderBy(c => c.CNOMBRECONCEPTO).ToListAsync(cancellationToken);

        foreach (admConceptos conceptoSql in coneptosSql)
        {
            var concepto = _mapper.Map<Concepto>(conceptoSql);

            BuscarObjectosRelacionados(concepto, conceptoSql);

            conceptosList.Add(concepto);
        }

        return conceptosList;
    }

    private void BuscarObjectosRelacionados(Concepto concepto, admConceptos conceptoSql)
    {
        concepto.DatosExtra = conceptoSql.ToDatosDictionary<admConceptos>();
    }
}
