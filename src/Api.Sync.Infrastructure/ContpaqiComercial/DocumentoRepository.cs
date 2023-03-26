using Api.Core.Domain.Common;
using Api.Core.Domain.Models;
using Api.Core.Domain.Requests;
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

        // ReSharper disable once CompareOfFloatsByEqualityOperator
        DocumentoSql? documentoSql = await _context.admDocumentos
            .Where(c => c.CIDCONCEPTODOCUMENTO == conceptoSql.CIDCONCEPTODOCUMENTO &&
                        c.CSERIEDOCUMENTO == llaveDocumento.Serie &&
                        c.CFOLIO == llaveDocumento.Folio)
            .ProjectTo<DocumentoSql>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(cancellationToken);

        if (documentoSql is null)
            return null;

        var documento = _mapper.Map<Documento>(documentoSql);

        await CargarObjectosRelacionadosAsync(documento, documentoSql, loadRelatedDataOptions, cancellationToken);

        return documento;
    }

    public async Task<IEnumerable<Documento>> BuscarPorRequestModelAsync(BuscarDocumentosRequestModel requestModel,
                                                                         ILoadRelatedDataOptions loadRelatedDataOptions,
                                                                         CancellationToken cancellationToken)
    {
        var documentosList = new List<Documento>();

        IQueryable<admDocumentos> documentosQuery = !string.IsNullOrWhiteSpace(requestModel.SqlQuery)
            ? _context.admDocumentos.FromSqlRaw($"SELECT * FROM admDocumentos WHERE {requestModel.SqlQuery}")
            : _context.admDocumentos.AsQueryable();

        if (requestModel.Id.HasValue)
            documentosQuery = documentosQuery.Where(d => d.CIDDOCUMENTO == requestModel.Id.Value);

        if (requestModel.Llave is not null)
        {
            int conceptoId = await _context.admConceptos.Where(c => c.CCODIGOCONCEPTO == requestModel.Llave.ConceptoCodigo)
                .Select(c => c.CIDCONCEPTODOCUMENTO)
                .FirstOrDefaultAsync(cancellationToken);

            // ReSharper disable once CompareOfFloatsByEqualityOperator
            documentosQuery = documentosQuery.Where(d =>
                d.CIDCONCEPTODOCUMENTO == conceptoId &&
                d.CSERIEDOCUMENTO == requestModel.Llave.Serie &&
                d.CFOLIO == requestModel.Llave.Folio);
        }

        if (!string.IsNullOrWhiteSpace(requestModel.ConceptoCodigo))
        {
            int conceptoId = await _context.admConceptos.Where(c => c.CCODIGOCONCEPTO == requestModel.ConceptoCodigo)
                .Select(c => c.CIDCONCEPTODOCUMENTO)
                .FirstOrDefaultAsync(cancellationToken);

            documentosQuery = documentosQuery.Where(d => d.CIDCONCEPTODOCUMENTO == conceptoId);
        }

        if (!string.IsNullOrWhiteSpace(requestModel.ClienteCodigo))
        {
            int clienteId = await _context.admClientes.Where(c => c.CCODIGOCLIENTE == requestModel.ClienteCodigo)
                .Select(c => c.CIDCLIENTEPROVEEDOR)
                .FirstOrDefaultAsync(cancellationToken);

            documentosQuery = documentosQuery.Where(d => d.CIDCONCEPTODOCUMENTO == clienteId);
        }

        if (requestModel.FechaInicio.HasValue)
        {
            var fechaInicio = requestModel.FechaInicio.Value.ToDateTime(TimeOnly.MinValue);
            documentosQuery = documentosQuery.Where(d => d.CFECHA >= fechaInicio);
        }

        if (requestModel.FechaFin.HasValue)
        {
            var fechaFin = requestModel.FechaFin.Value.ToDateTime(TimeOnly.MaxValue);
            documentosQuery = documentosQuery.Where(d => d.CFECHA <= fechaFin);
        }

        List<DocumentoSql> documentosSql = await documentosQuery.ProjectTo<DocumentoSql>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);

        foreach (DocumentoSql documentoSql in documentosSql)
        {
            var documento = _mapper.Map<Documento>(documentoSql);

            await CargarObjectosRelacionadosAsync(documento, documentoSql, loadRelatedDataOptions, cancellationToken);

            documentosList.Add(documento);
        }

        return documentosList;
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
