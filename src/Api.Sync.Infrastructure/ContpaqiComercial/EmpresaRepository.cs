using Api.Core.Domain.Common;
using Api.Sync.Core.Application.ContpaqiComercial.Interfaces;
using ARSoftware.Contpaqi.Comercial.Sdk.Abstractions.Dtos;
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
    private readonly IConfiguration _configuration;
    private readonly ContpaqiComercialGeneralesDbContext _context;
    private readonly IMapper _mapper;

    public EmpresaRepository(ContpaqiComercialGeneralesDbContext context, IMapper mapper, IConfiguration configuration)
    {
        _context = context;
        _mapper = mapper;
        _configuration = configuration;
    }

    public async Task<IEnumerable<Empresa>> BuscarTodoAsync(ILoadRelatedDataOptions relatedDataOptions, CancellationToken cancellationToken)
    {
        var empresasList = new List<Empresa>();

        List<EmpresaDto> empresasSql = await _context.Empresas.Where(e => e.CIDEMPRESA != 1)
            .OrderBy(m => m.CNOMBREEMPRESA)
            .ProjectTo<EmpresaDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);

        foreach (EmpresaDto? empresaSql in empresasSql)
        {
            var empresa = _mapper.Map<Empresa>(empresaSql);

            await CargarDatosRelacionadosAsync(empresa, relatedDataOptions, cancellationToken);

            empresasList.Add(empresa);
        }

        return empresasList;
    }

    private async Task CargarDatosRelacionadosAsync(Empresa empresa, ILoadRelatedDataOptions relatedDataOptions,
        CancellationToken cancellationToken)
    {
        var optionsBuilder = new DbContextOptionsBuilder<ContpaqiComercialEmpresaDbContext>();
        string empresaConnectionString = ContpaqiComercialSqlConnectionStringFactory.CreateContpaqiComercialEmpresaConnectionString(
            _configuration.GetConnectionString("Contpaqi")!, empresa.BaseDatos);
        optionsBuilder.UseSqlServer(empresaConnectionString);

        await using ContpaqiComercialEmpresaDbContext comercialEmpresaDbContext = new(optionsBuilder.Options);

        ParametrosDto parametrosSql = await comercialEmpresaDbContext.admParametros.ProjectTo<ParametrosDto>(_mapper.ConfigurationProvider)
            .FirstAsync(cancellationToken);

        empresa.Parametros = _mapper.Map<Parametros>(parametrosSql);

        if (relatedDataOptions.CargarDatosExtra)
            empresa.Parametros.DatosExtra = (await comercialEmpresaDbContext.admParametros.FirstAsync(cancellationToken))
                .ToDatosDictionary<admParametros>();
    }
}
