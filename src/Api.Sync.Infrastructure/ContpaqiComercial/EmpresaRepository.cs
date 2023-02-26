using Api.Core.Domain.Models;
using Api.Sync.Core.Application.ContpaqiComercial.Interfaces;
using ARSoftware.Contpaqi.Comercial.Sdk.Extras.Extensions;
using ARSoftware.Contpaqi.Comercial.Sql.Contexts;
using ARSoftware.Contpaqi.Comercial.Sql.Factories;
using ARSoftware.Contpaqi.Comercial.Sql.Models.Empresa;
using ARSoftware.Contpaqi.Comercial.Sql.Models.Generales;
using AutoMapper;
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

    public async Task<IEnumerable<Empresa>> BuscarTodoAsync(CancellationToken cancellationToken)
    {
        var empresasList = new List<Empresa>();

        List<Empresas> empresasSql = await _context.Empresas.AsNoTracking()
            .Where(e => e.CIDEMPRESA != 1)
            .OrderBy(m => m.CNOMBREEMPRESA)
            .ToListAsync(cancellationToken);

        foreach (Empresas empresaSql in empresasSql)
        {
            var empresa = _mapper.Map<Empresa>(empresaSql);

            await CargarObjectosRelacionadosAsync(empresa, cancellationToken);

            empresasList.Add(empresa);
        }

        return empresasList;
    }

    private async Task CargarObjectosRelacionadosAsync(Empresa empresa, CancellationToken cancellationToken)
    {
        var optionsBuilder = new DbContextOptionsBuilder<ContpaqiComercialEmpresaDbContext>();
        string empresaConnectionString = ContpaqiComercialSqlConnectionStringFactory.CreateContpaqiComercialEmpresaConnectionString(
            _configuration.GetConnectionString("Contpaqi"),
            empresa.BaseDatos);
        optionsBuilder.UseSqlServer(empresaConnectionString);

        _comercialEmpresaDbContext.Database.SetConnectionString(empresaConnectionString);

        admParametros parametros = await _comercialEmpresaDbContext.admParametros.AsNoTracking().FirstAsync(cancellationToken);
        empresa.GuidAdd = parametros.CGUIDDSL;
        empresa.Rfc = parametros.CRFCEMPRESA;
        empresa.DatosExtra = parametros.ToDatosDictionary<admParametros>();

        //using (var empresaContext = new ContpaqiComercialEmpresaDbContext(optionsBuilder.Options))
        //{
        //    var parametros = await empresaContext.admParametros.AsNoTracking()
        //        .Select(m => new { m.CGUIDDSL, m.CRFCEMPRESA })
        //        .FirstAsync(cancellationToken);
        //    empresa.GuidAdd = parametros.CGUIDDSL;
        //    empresa.Rfc = parametros.CRFCEMPRESA;
        //    empresa.DatosExtra = parametros.ToDatosDictionary<admParametros>();
        //}
    }
}
