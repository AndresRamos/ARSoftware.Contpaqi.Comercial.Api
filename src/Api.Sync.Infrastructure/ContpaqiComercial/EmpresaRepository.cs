using Api.Core.Domain.Common;
using Api.Core.Domain.Models;
using Api.Sync.Core.Application.ContpaqiComercial.Interfaces;
using Api.Sync.Infrastructure.ContpaqiComercial.Models;
using ARSoftware.Contpaqi.Comercial.Sdk.Extras.Extensions;
using ARSoftware.Contpaqi.Comercial.Sql.Contexts;
using ARSoftware.Contpaqi.Comercial.Sql.Factories;
using ARSoftware.Contpaqi.Comercial.Sql.Models.Empresa;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Api.Sync.Infrastructure.ContpaqiComercial;

public sealed class EmpresaRepository : IEmpresaRepository
{
    private readonly ContpaqiComercialEmpresaDbContext _comercialEmpresaDbContext;
    private readonly IConfiguration _configuration;
    private readonly ContpaqiComercialGeneralesDbContext _context;
    private readonly IMapper _mapper;

    public EmpresaRepository(ContpaqiComercialGeneralesDbContext context,
                             IMapper mapper,
                             IConfiguration configuration,
                             ContpaqiComercialEmpresaDbContext comercialEmpresaDbContext)
    {
        _context = context;
        _mapper = mapper;
        _configuration = configuration;
        _comercialEmpresaDbContext = comercialEmpresaDbContext;
    }

    public async Task<IEnumerable<Empresa>> BuscarTodoAsync(ILoadRelatedDataOptions relatedDataOptions, CancellationToken cancellationToken)
    {
        var empresasList = new List<Empresa>();

        List<EmpresaSql> empresasSql = await _context.Empresas.Where(e => e.CIDEMPRESA != 1)
            .OrderBy(m => m.CNOMBREEMPRESA)
            .ProjectTo<EmpresaSql>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);

        foreach (EmpresaSql? empresaSql in empresasSql)
        {
            var empresa = _mapper.Map<Empresa>(empresaSql);

            await CargarDatosRelacionadosAsync(empresa, relatedDataOptions, cancellationToken);

            empresasList.Add(empresa);
        }

        return empresasList;
    }

    private async Task CargarDatosRelacionadosAsync(Empresa empresa,
                                                    ILoadRelatedDataOptions relatedDataOptions,
                                                    CancellationToken cancellationToken)
    {
        var optionsBuilder = new DbContextOptionsBuilder<ContpaqiComercialEmpresaDbContext>();
        string empresaConnectionString = ContpaqiComercialSqlConnectionStringFactory.CreateContpaqiComercialEmpresaConnectionString(
            _configuration.GetConnectionString("Contpaqi"),
            empresa.BaseDatos);
        optionsBuilder.UseSqlServer(empresaConnectionString);

        _comercialEmpresaDbContext.Database.SetConnectionString(empresaConnectionString);

        ParametrosSql parametrosSql = await _comercialEmpresaDbContext.admParametros.ProjectTo<ParametrosSql>(_mapper.ConfigurationProvider)
            .FirstAsync(cancellationToken);

        empresa.GuidAdd = parametrosSql.CGUIDDSL;
        empresa.Rfc = parametrosSql.CRFCEMPRESA;

        if (relatedDataOptions.CargarDatosExtra)
            empresa.DatosExtra = (await _comercialEmpresaDbContext.admParametros.FirstAsync(cancellationToken))
                .ToDatosDictionary<admParametros>();
    }
}
