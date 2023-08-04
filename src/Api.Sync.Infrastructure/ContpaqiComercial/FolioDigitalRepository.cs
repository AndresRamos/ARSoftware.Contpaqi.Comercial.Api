using Api.Core.Domain.Common;
using Api.Sync.Core.Application.ContpaqiComercial.Interfaces;
using ARSoftware.Contpaqi.Comercial.Sdk.Abstractions.Dtos;
using ARSoftware.Contpaqi.Comercial.Sdk.Extras.Extensions;
using ARSoftware.Contpaqi.Comercial.Sql.Contexts;
using ARSoftware.Contpaqi.Comercial.Sql.Models.Empresa;
using AutoMapper;
using AutoMapper.QueryableExtensions;
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

    public async Task<FolioDigital?> BuscarPorDocumentoIdAsync(int conceptoId, int documentoId,
        ILoadRelatedDataOptions loadRelatedDataOptions, CancellationToken cancellationToken)
    {
        FolioDigitalDto? folioDigitalSql = await _context.admFoliosDigitales
            .Where(m => m.CIDCPTODOC == conceptoId && m.CIDDOCTO == documentoId)
            .ProjectTo<FolioDigitalDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(cancellationToken);

        if (folioDigitalSql is null) return null;

        var folioDigital = _mapper.Map<FolioDigital>(folioDigitalSql);

        await CargarDatosRelacionadosAsync(folioDigital, folioDigitalSql, loadRelatedDataOptions, cancellationToken);

        return folioDigital;
    }

    private async Task CargarDatosRelacionadosAsync(FolioDigital folioDigital, FolioDigitalDto folioDigitalSql,
        ILoadRelatedDataOptions loadRelatedDataOptions, CancellationToken cancellationToken)
    {
        if (loadRelatedDataOptions.CargarDatosExtra)
        {
            folioDigital.DatosExtra =
                (await _context.admFoliosDigitales.FirstAsync(m => m.CIDFOLDIG == folioDigitalSql.CIDFOLDIG, cancellationToken))
                .ToDatosDictionary<admFoliosDigitales>();
        }
    }
}
