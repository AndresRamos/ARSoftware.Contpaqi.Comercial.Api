using Api.Core.Domain.Models;
using Api.Sync.Core.Application.ContpaqiComercial.Interfaces;
using ARSoftware.Contpaqi.Comercial.Sdk.DatosAbstractos;
using ARSoftware.Contpaqi.Comercial.Sdk.Extras.Extensions;
using ARSoftware.Contpaqi.Comercial.Sql.Contexts;
using ARSoftware.Contpaqi.Comercial.Sql.Models.Empresa;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace Api.Sync.Infrastructure.ContpaqiComercial;

public sealed class DocumentoRepository : IDocumentoRepository
{
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
    }

    public async Task<Documento> BuscarPorIdAsync(int id, CancellationToken cancellationToken)
    {
        admDocumentos documentoSql = await _context.admDocumentos.FirstAsync(c => c.CIDDOCUMENTO == id, cancellationToken);

        var documento = _mapper.Map<Documento>(documentoSql);

        await CargarObjectosRelacionadosAsync(documento, documentoSql, cancellationToken);

        return documento;
    }

    public async Task<tLlaveDoc> BuscarLlavePorIdAsync(int id, CancellationToken cancellationToken)
    {
        var documentoSql = await _context.admDocumentos.Where(m => m.CIDDOCUMENTO == id)
            .Select(m => new { m.CIDCONCEPTODOCUMENTO, m.CSERIEDOCUMENTO, m.CFOLIO })
            .FirstAsync(cancellationToken);

        string? conceptoSql = await _context.admConceptos.Where(m => m.CIDCONCEPTODOCUMENTO == documentoSql.CIDCONCEPTODOCUMENTO)
            .Select(m => m.CCODIGOCONCEPTO)
            .FirstAsync(cancellationToken);

        return new tLlaveDoc { aCodConcepto = conceptoSql, aSerie = documentoSql.CSERIEDOCUMENTO, aFolio = documentoSql.CFOLIO };
    }

    public async Task<Documento> BuscarPorLlaveAsync(LlaveDocumento llaveDocumento, CancellationToken cancellationToken)
    {
        admConceptos conceptoSql = await _context.admConceptos.FirstAsync(c => c.CCODIGOCONCEPTO == llaveDocumento.ConceptoCodigo,
            cancellationToken);

        admDocumentos documentoSql = await _context.admDocumentos.FirstAsync(c =>
                c.CIDCONCEPTODOCUMENTO == conceptoSql.CIDCONCEPTODOCUMENTO &&
                c.CSERIEDOCUMENTO == llaveDocumento.Serie &&
                // ReSharper disable once CompareOfFloatsByEqualityOperator
                c.CFOLIO == llaveDocumento.Folio,
            cancellationToken);

        var documento = _mapper.Map<Documento>(documentoSql);

        await CargarObjectosRelacionadosAsync(documento, documentoSql, cancellationToken);

        return documento;
    }

    public async Task<int> BusarIdPorLlaveAsync(LlaveDocumento llaveDocumento, CancellationToken cancellationToken)
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

    private async Task CargarObjectosRelacionadosAsync(Documento documento, admDocumentos documentoSql, CancellationToken cancellationToken)
    {
        documento.Concepto = await _conceptoRepository.BuscarPorIdAsync(documentoSql.CIDCONCEPTODOCUMENTO, cancellationToken) ??
                             new Concepto();
        documento.Cliente = await _clienteRepository.BuscarPorIdAsync(documentoSql.CIDCLIENTEPROVEEDOR, cancellationToken) ?? new Cliente();

        documento.Movimientos =
            (await _movimientoRepository.BuscarPorDocumentoIdAsync(documentoSql.CIDDOCUMENTO, cancellationToken)).ToList();

        documento.FolioDigital =
            await _folioDigitalRepository.BuscarPorDocumentoIdAsync(documentoSql.CIDCONCEPTODOCUMENTO,
                documentoSql.CIDDOCUMENTO,
                cancellationToken) ??
            new FolioDigital();

        documento.DatosExtra = documentoSql.ToDatosDictionary<admDocumentos>();
    }
}
