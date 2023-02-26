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
    private readonly ContpaqiComercialEmpresaDbContext _context;
    private readonly IMapper _mapper;

    public DocumentoRepository(ContpaqiComercialEmpresaDbContext context, IMapper mapper, IClienteRepository clienteRepository)
    {
        _context = context;
        _mapper = mapper;
        _clienteRepository = clienteRepository;
    }

    public async Task<Documento> BuscarPorIdAsync(int id, CancellationToken cancellationToken)
    {
        admDocumentos documentoSql = await _context.admDocumentos.AsNoTracking().FirstAsync(c => c.CIDDOCUMENTO == id, cancellationToken);

        var documento = _mapper.Map<Documento>(documentoSql);

        await CargarObjectosRelacionadosAsync(documento, documentoSql, cancellationToken);

        return documento;
    }

    public async Task<tLlaveDoc> BuscarLlavePorIdAsync(int id, CancellationToken cancellationToken)
    {
        var documentoSql = await _context.admDocumentos.AsNoTracking()
            .Where(m => m.CIDDOCUMENTO == id)
            .Select(m => new { m.CIDCONCEPTODOCUMENTO, m.CSERIEDOCUMENTO, m.CFOLIO })
            .FirstAsync(cancellationToken);

        string? conceptoSql = await _context.admConceptos.AsNoTracking()
            .Where(m => m.CIDCONCEPTODOCUMENTO == documentoSql.CIDCONCEPTODOCUMENTO)
            .Select(m => m.CCODIGOCONCEPTO)
            .FirstAsync(cancellationToken);

        return new tLlaveDoc { aCodConcepto = conceptoSql, aSerie = documentoSql.CSERIEDOCUMENTO, aFolio = documentoSql.CFOLIO };
    }

    public async Task<Documento> BuscarPorLlaveAsync(LlaveDocumento llaveDocumento, CancellationToken cancellationToken)
    {
        admConceptos conceptoSql = await _context.admConceptos.FirstAsync(c => c.CCODIGOCONCEPTO == llaveDocumento.ConceptoCodigo,
            cancellationToken);

        admDocumentos documentoSql = await _context.admDocumentos.AsNoTracking()
            .FirstAsync(c => c.CIDCONCEPTODOCUMENTO == conceptoSql.CIDCONCEPTODOCUMENTO &&
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

        admDocumentos documentoSql = await _context.admDocumentos.AsNoTracking()
            .FirstAsync(c => c.CIDCONCEPTODOCUMENTO == conceptoSql.CIDCONCEPTODOCUMENTO &&
                             c.CSERIEDOCUMENTO == llaveDocumento.Serie &&
                             // ReSharper disable once CompareOfFloatsByEqualityOperator
                             c.CFOLIO == llaveDocumento.Folio,
                cancellationToken);

        return documentoSql.CIDDOCUMENTO;
    }

    private async Task CargarObjectosRelacionadosAsync(Documento documento, admDocumentos documentoSql, CancellationToken cancellationToken)
    {
        admConceptos admConcepto =
            await _context.admConceptos.FirstAsync(c => c.CIDCONCEPTODOCUMENTO == documentoSql.CIDCONCEPTODOCUMENTO, cancellationToken);

        documento.Concepto = _mapper.Map<Concepto>(admConcepto);
        documento.Cliente = await _clienteRepository.BuscarPorIdAsync(documentoSql.CIDCLIENTEPROVEEDOR, cancellationToken) ?? new Cliente();

        foreach (admMovimientos admMovimiento in await _context.admMovimientos.Where(m => m.CIDDOCUMENTO == documentoSql.CIDDOCUMENTO)
                     .ToListAsync(cancellationToken))
        {
            admProductos admProducto =
                await _context.admProductos.FirstAsync(p => p.CIDPRODUCTO == admMovimiento.CIDPRODUCTO, cancellationToken);
            admAlmacenes admAlmacene =
                await _context.admAlmacenes.FirstAsync(p => p.CIDALMACEN == admMovimiento.CIDALMACEN, cancellationToken);

            var movimiento = _mapper.Map<Movimiento>(admMovimiento);
            movimiento.Producto = _mapper.Map<Producto>(admProducto);
            movimiento.Almacen = _mapper.Map<Almacen>(admAlmacene);

            documento.Movimientos.Add(movimiento);
        }

        documento.DatosExtra = documentoSql.ToDatosDictionary<admDocumentos>();
    }
}
