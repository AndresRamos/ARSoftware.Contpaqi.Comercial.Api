using Api.Core.Domain.Models;
using Api.Sync.Core.Application.ContpaqiComercial.Interfaces;
using ARSoftware.Contpaqi.Comercial.Sdk.DatosAbstractos;
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

    public async Task<Documento> BuscarDocumentoPorIdAsync(int id, CancellationToken cancellationToken)
    {
        admDocumentos admDocumento = await _context.admDocumentos.AsNoTracking().FirstAsync(c => c.CIDDOCUMENTO == id, cancellationToken);

        var documento = _mapper.Map<Documento>(admDocumento);

        await LoadRelatedProperties(admDocumento, documento, cancellationToken);

        return documento;
    }

    public async Task<tLlaveDoc> BuscarLlavePorIdAsync(int id, CancellationToken cancellationToken)
    {
        var admDocumento = await _context.admDocumentos.AsNoTracking()
            .Where(m => m.CIDDOCUMENTO == id)
            .Select(m => new { m.CIDCONCEPTODOCUMENTO, m.CSERIEDOCUMENTO, m.CFOLIO })
            .FirstAsync(cancellationToken);

        string? concepto = await _context.admConceptos.AsNoTracking()
            .Where(m => m.CIDCONCEPTODOCUMENTO == admDocumento.CIDCONCEPTODOCUMENTO)
            .Select(m => m.CCODIGOCONCEPTO)
            .FirstAsync(cancellationToken);

        return new tLlaveDoc { aCodConcepto = concepto, aSerie = admDocumento.CSERIEDOCUMENTO, aFolio = admDocumento.CFOLIO };
    }

    private async Task LoadRelatedProperties(admDocumentos admDocumento, Documento documento, CancellationToken cancellationToken)
    {
        admConceptos admConcepto =
            await _context.admConceptos.FirstAsync(c => c.CIDCONCEPTODOCUMENTO == admDocumento.CIDCONCEPTODOCUMENTO, cancellationToken);
        admClientes admCliente =
            await _context.admClientes.FirstAsync(c => c.CIDCLIENTEPROVEEDOR == admDocumento.CIDCLIENTEPROVEEDOR, cancellationToken);

        documento.Concepto = _mapper.Map<Concepto>(admConcepto);
        documento.Cliente = await _clienteRepository.BuscarPorIdAsync(admDocumento.CIDCLIENTEPROVEEDOR, cancellationToken);

        foreach (admMovimientos admMovimiento in await _context.admMovimientos.Where(m => m.CIDDOCUMENTO == admDocumento.CIDDOCUMENTO)
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
    }
}
