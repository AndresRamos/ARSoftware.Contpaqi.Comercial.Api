using Api.Core.Domain.Models;
using Api.Sync.Core.Application.ContpaqiComercial.Interfaces;
using ARSoftware.Contpaqi.Comercial.Sdk.Extras.Extensions;
using ARSoftware.Contpaqi.Comercial.Sql.Contexts;
using ARSoftware.Contpaqi.Comercial.Sql.Models.Empresa;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace Api.Sync.Infrastructure.ContpaqiComercial;

public sealed class FolioDigitalRepository : IFolioDigitalRepository
{
    private readonly ContpaqiComercialEmpresaDbContext _context;
    private readonly IMapper _mapper;

    public FolioDigitalRepository(ContpaqiComercialEmpresaDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<FolioDigital?> BuscarPorDocumentoIdAsync(int conceptoId, int documentoId, CancellationToken cancellationToken)
    {
        admFoliosDigitales? folioDigitalSql = await _context.admFoliosDigitales
            .Where(m => m.CIDCPTODOC == conceptoId && m.CIDDOCTO == documentoId)
            .FirstOrDefaultAsync(cancellationToken);

        if (folioDigitalSql is null)
            return null;

        var folioDigital = _mapper.Map<FolioDigital>(folioDigitalSql);

        CargarObjectosRelacionados(folioDigital, folioDigitalSql);

        return folioDigital;
    }

    private void CargarObjectosRelacionados(FolioDigital folioDigital, admFoliosDigitales folioDigitalSql)
    {
        folioDigital.DatosExtra = folioDigitalSql.ToDatosDictionary<admFoliosDigitales>();
    }
}
