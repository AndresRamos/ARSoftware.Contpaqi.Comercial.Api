using Api.Core.Domain.Common;
using Api.Core.Domain.Models;
using Api.Sync.Core.Application.ContpaqiComercial.Interfaces;
using Api.Sync.Infrastructure.ContpaqiComercial.Models;
using ARSoftware.Contpaqi.Comercial.Sdk.Extras.Extensions;
using ARSoftware.Contpaqi.Comercial.Sql.Contexts;
using ARSoftware.Contpaqi.Comercial.Sql.Models.Empresa;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace Api.Sync.Infrastructure.ContpaqiComercial;

public sealed class DocumentoRepository : IDocumentoRepository
{
    private readonly IAgenteRepository _agenteRepository;
    private readonly IClienteRepository _clienteRepository;
    private readonly IConceptoRepository _conceptoRepository;
    private readonly ContpaqiComercialEmpresaDbContext _context;
    private readonly IFolioDigitalRepository _folioDigitalRepository;
    private readonly IMapper _mapper;
    private readonly IMovimientoRepository _movimientoRepository;

    public DocumentoRepository(ContpaqiComercialEmpresaDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
        _clienteRepository = new ClienteRepository(context, mapper);
        _folioDigitalRepository = new FolioDigitalRepository(context, mapper);
        _conceptoRepository = new ConceptoRepository(context, mapper);
        _movimientoRepository = new MovimientoRepository(context, mapper);
        _agenteRepository = new AgenteRepository(context, mapper);
    }

    public async Task<Documento?> BuscarPorIdAsync(int id,
                                                   ILoadRelatedDataOptions loadRelatedDataOptions,
                                                   CancellationToken cancellationToken)
    {
        DocumentoSql? documentoSql = await _context.admDocumentos.Where(c => c.CIDDOCUMENTO == id)
            .ProjectTo<DocumentoSql>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(cancellationToken);

        if (documentoSql is null)
            return null;

        var documento = _mapper.Map<Documento>(documentoSql);

        await CargarObjectosRelacionadosAsync(documento, documentoSql, loadRelatedDataOptions, cancellationToken);

        return documento;
    }

    public async Task<Documento?> BuscarPorLlaveAsync(LlaveDocumento llaveDocumento,
                                                      ILoadRelatedDataOptions loadRelatedDataOptions,
                                                      CancellationToken cancellationToken)
    {
        ConceptoSql? conceptoSql = await _context.admConceptos.Where(c => c.CCODIGOCONCEPTO == llaveDocumento.ConceptoCodigo)
            .ProjectTo<ConceptoSql>(_mapper.ConfigurationProvider)
            .FirstAsync(cancellationToken);

        DocumentoSql? documentoSql = await _context.admDocumentos.Where(c => c.CIDCONCEPTODOCUMENTO == conceptoSql.CIDCONCEPTODOCUMENTO &&
                                                                             c.CSERIEDOCUMENTO == llaveDocumento.Serie &&
                                                                             // ReSharper disable once CompareOfFloatsByEqualityOperator
                                                                             c.CFOLIO == llaveDocumento.Folio)
            .ProjectTo<DocumentoSql>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(cancellationToken);

        if (documentoSql is null)
            return null;

        var documento = _mapper.Map<Documento>(documentoSql);

        await CargarObjectosRelacionadosAsync(documento, documentoSql, loadRelatedDataOptions, cancellationToken);

        return documento;
    }

    public async Task<int> BusarIdPorLlaveAsync(LlaveDocumento llaveDocumento,
                                                ILoadRelatedDataOptions loadRelatedDataOptions,
                                                CancellationToken cancellationToken)
    {
        admConceptos conceptoSql = await _context.admConceptos.FirstAsync(c => c.CCODIGOCONCEPTO == llaveDocumento.ConceptoCodigo,
            cancellationToken);

        admDocumentos documentoSql = await _context.admDocumentos.FirstAsync(c =>
                c.CIDCONCEPTODOCUMENTO == conceptoSql.CIDCONCEPTODOCUMENTO &&
                c.CSERIEDOCUMENTO == llaveDocumento.Serie &&
                // ReSharper disable once CompareOfFloatsByEqualityOperator
                c.CFOLIO == llaveDocumento.Folio,
            cancellationToken);

        return documentoSql.CIDDOCUMENTO;
    }

    private async Task CargarObjectosRelacionadosAsync(Documento documento,
                                                       DocumentoSql documentoSql,
                                                       ILoadRelatedDataOptions loadRelatedDataOptions,
                                                       CancellationToken cancellationToken)
    {
        documento.Concepto =
            await _conceptoRepository.BuscarPorIdAsync(documentoSql.CIDCONCEPTODOCUMENTO, loadRelatedDataOptions, cancellationToken) ??
            new Concepto();

        documento.Cliente =
            await _clienteRepository.BuscarPorIdAsync(documentoSql.CIDCLIENTEPROVEEDOR, loadRelatedDataOptions, cancellationToken);

        documento.Agente = await _agenteRepository.BuscarPorIdAsync(documentoSql.CIDAGENTE, loadRelatedDataOptions, cancellationToken);

        documento.Movimientos =
            (await _movimientoRepository.BuscarPorDocumentoIdAsync(documentoSql.CIDDOCUMENTO, loadRelatedDataOptions, cancellationToken))
            .ToList();

        documento.FolioDigital = await _folioDigitalRepository.BuscarPorDocumentoIdAsync(documentoSql.CIDCONCEPTODOCUMENTO,
            documentoSql.CIDDOCUMENTO,
            loadRelatedDataOptions,
            cancellationToken);

        if (loadRelatedDataOptions.CargarDatosExtra)
            documento.DatosExtra =
                (await _context.admDocumentos.FirstAsync(m => m.CIDDOCUMENTO == documentoSql.CIDDOCUMENTO, cancellationToken))
                .ToDatosDictionary<admDocumentos>();
    }
}
