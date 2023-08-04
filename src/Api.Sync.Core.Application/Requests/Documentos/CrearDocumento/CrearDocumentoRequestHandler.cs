using Api.Core.Domain.Requests;
using Api.Sync.Core.Application.ContpaqiComercial.Interfaces;
using ARSoftware.Contpaqi.Comercial.Sdk.DatosAbstractos;
using ARSoftware.Contpaqi.Comercial.Sdk.Extras.Interfaces;
using MediatR;

namespace Api.Sync.Core.Application.Requests.Documentos.CrearDocumento;

public sealed class CrearDocumentoRequestHandler : IRequestHandler<CrearDocumentoRequest, CrearDocumentoResponse>
{
    private readonly IDocumentoRepository _documentoRepository;
    private readonly IDocumentoService _documentoService;
    private readonly IMovimientoService _movimientoService;
    private int _documentoSdkId;

    public CrearDocumentoRequestHandler(IDocumentoService documentoService, IMovimientoService movimientoService,
        IDocumentoRepository documentoRepository)
    {
        _documentoService = documentoService;
        _movimientoService = movimientoService;
        _documentoRepository = documentoRepository;
    }

    public async Task<CrearDocumentoResponse> Handle(CrearDocumentoRequest request, CancellationToken cancellationToken)
    {
        Documento documento = request.Model.Documento;

        try
        {
            if (request.Options.UsarFechaDelDia) documento.Fecha = DateTime.Today;

            if (request.Options.BuscarSiguienteFolio)
            {
                tLlaveDoc siguienteFolio = _documentoService.BuscarSiguienteSerieYFolio(documento.Concepto.Codigo);
                documento.Serie = siguienteFolio.aSerie;
                documento.Folio = (int)siguienteFolio.aFolio;
            }

            _documentoSdkId = _documentoService.Crear(documento);

            foreach (Movimiento movimiento in documento.Movimientos) _movimientoService.Crear(_documentoSdkId, movimiento);

            Documento documentoCreado = await _documentoRepository.BuscarPorIdAsync(_documentoSdkId, request.Options, cancellationToken) ??
                                        throw new InvalidOperationException();

            return CrearDocumentoResponse.CreateInstance(documentoCreado);
        }
        finally
        {
            if (_documentoSdkId is not 0) _documentoService.DesbloquearDocumento(_documentoSdkId);
        }
    }
}
