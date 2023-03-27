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

public sealed class AgenteRepository : IAgenteRepository
{
    private readonly ContpaqiComercialEmpresaDbContext _context;
    private readonly IMapper _mapper;

    public AgenteRepository(ContpaqiComercialEmpresaDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Agente?> BuscarPorIdAsync(int id, ILoadRelatedDataOptions loadRelatedDataOptions, CancellationToken cancellationToken)
    {
        AgenteSql? agenteSql = await _context.admAgentes.Where(m => m.CIDAGENTE == id)
            .ProjectTo<AgenteSql>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(cancellationToken);

        if (agenteSql is null)
            return null;

        var agente = _mapper.Map<Agente>(agenteSql);

        await CargarDatosRelacionadosAsync(agente, agenteSql, loadRelatedDataOptions, cancellationToken);

        return agente;
    }

    public async Task<Agente?> BuscarPorCodigoAsync(string codigo,
                                                    ILoadRelatedDataOptions loadRelatedDataOptions,
                                                    CancellationToken cancellationToken)
    {
        AgenteSql? agenteSql = await _context.admAgentes.Where(m => m.CCODIGOAGENTE == codigo)
            .ProjectTo<AgenteSql>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(cancellationToken);

        if (agenteSql is null)
            return null;

        var agente = _mapper.Map<Agente>(agenteSql);

        await CargarDatosRelacionadosAsync(agente, agenteSql, loadRelatedDataOptions, cancellationToken);

        return agente;
    }

    public async Task<IEnumerable<Agente>> BuscarPorRequestModelAsync(BuscarAgentesRequestModel requestModel,
                                                                      ILoadRelatedDataOptions loadRelatedDataOptions,
                                                                      CancellationToken cancellationToken)
    {
        var agentesList = new List<Agente>();

        IQueryable<admAgentes> agentesQuery = !string.IsNullOrWhiteSpace(requestModel.SqlQuery)
            ? _context.admAgentes.FromSqlRaw($"SELECT * FROM admAgentes WHERE {requestModel.SqlQuery}")
            : _context.admAgentes.AsQueryable();

        if (requestModel.Id is not null)
            agentesQuery = agentesQuery.Where(a => a.CIDAGENTE == requestModel.Id);

        if (!string.IsNullOrWhiteSpace(requestModel.Codigo))
            agentesQuery = agentesQuery.Where(a => a.CCODIGOAGENTE == requestModel.Codigo);

        List<AgenteSql> agentesSql = await agentesQuery.ProjectTo<AgenteSql>(_mapper.ConfigurationProvider).ToListAsync(cancellationToken);

        foreach (AgenteSql agenteSql in agentesSql)
        {
            var agente = _mapper.Map<Agente>(agenteSql);

            await CargarDatosRelacionadosAsync(agente, agenteSql, loadRelatedDataOptions, cancellationToken);

            agentesList.Add(agente);
        }

        return agentesList;
    }

    public async Task<IEnumerable<Agente>> BuscarTodoAsync(ILoadRelatedDataOptions loadRelatedDataOptions,
                                                           CancellationToken cancellationToken)
    {
        var agentesList = new List<Agente>();

        List<AgenteSql> agentesSql = await _context.admAgentes.OrderBy(c => c.CNOMBREAGENTE)
            .ProjectTo<AgenteSql>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);

        foreach (AgenteSql agenteSql in agentesSql)
        {
            var agente = _mapper.Map<Agente>(agenteSql);

            await CargarDatosRelacionadosAsync(agente, agenteSql, loadRelatedDataOptions, cancellationToken);

            agentesList.Add(agente);
        }

        return agentesList;
    }

    private async Task CargarDatosRelacionadosAsync(Agente agente,
                                                    AgenteSql agenteSql,
                                                    ILoadRelatedDataOptions loadRelatedDataOptions,
                                                    CancellationToken cancellationToken)
    {
        if (loadRelatedDataOptions.CargarDatosExtra)
            agente.DatosExtra = (await _context.admAgentes.FirstAsync(m => m.CIDAGENTE == agenteSql.CIDAGENTE, cancellationToken))
                .ToDatosDictionary<admAgentes>();
    }
}
