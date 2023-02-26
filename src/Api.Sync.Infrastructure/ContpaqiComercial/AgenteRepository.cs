using Api.Core.Domain.Models;
using Api.Sync.Core.Application.ContpaqiComercial.Interfaces;
using ARSoftware.Contpaqi.Comercial.Sdk.Extras.Extensions;
using ARSoftware.Contpaqi.Comercial.Sql.Contexts;
using ARSoftware.Contpaqi.Comercial.Sql.Models.Empresa;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace Api.Sync.Infrastructure.ContpaqiComercial;

public sealed class AgenteRepository : IAgenteRepository
{
    private readonly ContpaqiComercialEmpresaDbContext _context;
    private readonly IMapper _mapper;

    public AgenteRepository(ContpaqiComercialEmpresaDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Agente?> BuscarPorIdAsync(int id, CancellationToken cancellationToken)
    {
        admAgentes? agenteSql = await _context.admAgentes.FirstOrDefaultAsync(m => m.CIDAGENTE == id, cancellationToken);

        if (agenteSql is null)
            return null;

        var agente = _mapper.Map<Agente>(agenteSql);

        BuscarObjectosRelacionados(agente, agenteSql);

        return agente;
    }

    public async Task<Agente?> BuscarPorCodigoAsync(string codigo, CancellationToken cancellationToken)
    {
        admAgentes? agenteSql = await _context.admAgentes.FirstOrDefaultAsync(m => m.CCODIGOAGENTE == codigo, cancellationToken);

        if (agenteSql is null)
            return null;

        var agente = _mapper.Map<Agente>(agenteSql);

        BuscarObjectosRelacionados(agente, agenteSql);

        return agente;
    }

    public async Task<IEnumerable<Agente>> BuscarTodoAsync(CancellationToken cancellationToken)
    {
        var agentesList = new List<Agente>();

        List<admAgentes> agentesSql = await _context.admAgentes.OrderBy(c => c.CNOMBREAGENTE).ToListAsync(cancellationToken);

        foreach (admAgentes agenteSql in agentesSql)
        {
            var agente = _mapper.Map<Agente>(agenteSql);

            BuscarObjectosRelacionados(agente, agenteSql);

            agentesList.Add(agente);
        }

        return agentesList;
    }

    private void BuscarObjectosRelacionados(Agente agente, admAgentes agenteSql)
    {
        agente.DatosExtra = agenteSql.ToDatosDictionary<admAgentes>();
    }
}
