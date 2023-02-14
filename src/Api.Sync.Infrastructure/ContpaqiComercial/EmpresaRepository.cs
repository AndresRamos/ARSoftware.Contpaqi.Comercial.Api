using Api.Sync.Core.Application.ContpaqiComercial.Interfaces;
using Api.Sync.Core.Application.ContpaqiComercial.Models;
using ARSoftware.Contpaqi.Comercial.Sql.Contexts;
using ARSoftware.Contpaqi.Comercial.Sql.Factories;
using ARSoftware.Contpaqi.Comercial.Sql.Models.Generales;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Api.Sync.Infrastructure.ContpaqiComercial;

public sealed class EmpresaRepository : IEmpresaRepository
{
    private readonly IConfiguration _configuration;
    private readonly ContpaqiComercialGeneralesDbContext _context;
    private readonly IMapper _mapper;

    public EmpresaRepository(ContpaqiComercialGeneralesDbContext context, IMapper mapper, IConfiguration configuration)
    {
        _context = context;
        _mapper = mapper;
        _configuration = configuration;
    }

    public async Task<IEnumerable<EmpresaDto>> BuscarEmpresasAsync(CancellationToken cancellationToken)
    {
        List<Empresas> empresasComercial = await _context.Empresas.AsNoTracking()
            .Where(e => e.CIDEMPRESA != 1)
            .OrderBy(m => m.CNOMBREEMPRESA)
            .ToListAsync(cancellationToken);

        var empresas = _mapper.Map<List<EmpresaDto>>(empresasComercial);

        foreach (EmpresaDto empresa in empresas)
            await BuscarDatosExtraAsync(empresa, cancellationToken);

        return empresas;
    }

    private async Task BuscarDatosExtraAsync(EmpresaDto empresa, CancellationToken cancellationToken)
    {
        var optionsBuilder = new DbContextOptionsBuilder<ContpaqiComercialEmpresaDbContext>();
        optionsBuilder.UseSqlServer(ContpaqiComercialSqlConnectionStringFactory.CreateContpaqiComercialEmpresaConnectionString(
            _configuration.GetConnectionString("Contpaqi"),
            empresa.BaseDatos));

        using (var empresaContext = new ContpaqiComercialEmpresaDbContext(optionsBuilder.Options))
        {
            var parametros = await empresaContext.admParametros.Select(m => new { m.CGUIDDSL, m.CRFCEMPRESA })
                .FirstAsync(cancellationToken);
            empresa.GuidAdd = parametros.CGUIDDSL;
            empresa.Rfc = parametros.CRFCEMPRESA;
        }
    }
}
