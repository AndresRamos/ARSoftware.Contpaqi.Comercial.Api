using Api.Core.Domain.Common;
using Api.Core.Domain.Models;
using Api.Core.Domain.Requests;
using Api.Sync.Core.Application.ContpaqiComercial.Interfaces;
using ARSoftware.Contpaqi.Comercial.Sdk.DatosAbstractos;
using ARSoftware.Contpaqi.Comercial.Sdk.Extras.Extensions;
using ARSoftware.Contpaqi.Comercial.Sdk.Extras.Helpers;
using ARSoftware.Contpaqi.Comercial.Sdk.Extras.Interfaces;
using ARSoftware.Contpaqi.Comercial.Sql.Models.Empresa;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Api.Sync.Core.Application.Requests.Documentos.CrearDocumento;

public sealed class CrearDocumentoRequestHandler : IRequestHandler<CrearDocumentoRequest, ApiResponse>
{
    private readonly IDocumentoRepository _documentoRepository;
    private readonly IDocumentoService _documentoService;
    private readonly ILogger _logger;
    private readonly IMapper _mapper;
    private readonly IMovimientoService _movimientoService;
    private int _documentoSdkId;

    public CrearDocumentoRequestHandler(IDocumentoService documentoService, IMapper mapper, IMovimientoService movimientoService,
        ILogger<CrearDocumentoRequestHandler> logger, IDocumentoRepository documentoRepository)
    {
        _documentoService = documentoService;
        _mapper = mapper;
        _movimientoService = movimientoService;
        _logger = logger;
        _documentoRepository = documentoRepository;
    }

    public async Task<ApiResponse> Handle(CrearDocumentoRequest request, CancellationToken cancellationToken)
    {
        Documento documento = request.Model.Documento;

        try
        {
            var documentoSdk = _mapper.Map<tDocumento>(documento);
            if (request.Options.UsarFechaDelDia)
                documentoSdk.aFecha = DateTime.Today.ToSdkFecha();

            if (request.Options.BuscarSiguienteFolio)
            {
                tLlaveDoc siguienteFolio = _documentoService.BuscarSiguienteSerieYFolio(documentoSdk.aCodConcepto);
                documentoSdk.aSerie = siguienteFolio.aSerie;
                documentoSdk.aFolio = siguienteFolio.aFolio;
            }

            _documentoSdkId = _documentoService.Crear(documentoSdk);

            var datosDocumento = new Dictionary<string, string>(documento.DatosExtra);

            datosDocumento.TryAdd(nameof(admDocumentos.COBSERVACIONES), documento.Observaciones);

            if (documento.FormaPago is not null)
                datosDocumento.TryAdd(nameof(admDocumentos.CMETODOPAG), documento.FormaPago.Clave);

            if (documento.MetodoPago is not null)
                datosDocumento.TryAdd(nameof(admDocumentos.CCANTPARCI),
                    MetodoPagoHelper.ConvertToSdkValue(documento.MetodoPago).ToString());

            _documentoService.Actualizar(_documentoSdkId, datosDocumento);

            foreach (Movimiento movimiento in documento.Movimientos)
            {
                var movimientoSdk = _mapper.Map<tMovimiento>(movimiento);
                // Todo: Validar el tipo de movimiento para saber si es nomal, de descuento, o de series/capas/pedimentos
                int movimientoSdkId = _movimientoService.Crear(_documentoSdkId, movimientoSdk);

                var datosMovimiento = new Dictionary<string, string>(movimiento.DatosExtra);

                datosMovimiento.TryAdd(nameof(admMovimientos.COBSERVAMOV), movimiento.Observaciones);

                _movimientoService.Actualizar(movimientoSdkId, datosMovimiento);

                foreach (SeriesCapas movimientoSeriesCapas in movimiento.SeriesCapas)
                    _movimientoService.CrearSeriesCapas(movimientoSdkId, _mapper.Map<tSeriesCapas>(movimientoSeriesCapas));
            }

            var responseModel = new CrearDocumentoResponseModel
            {
                Documento = (await _documentoRepository.BuscarPorIdAsync(_documentoSdkId, request.Options, cancellationToken))!
            };

            return ApiResponse.CreateSuccessfull<CrearDocumentoResponse, CrearDocumentoResponseModel>(responseModel);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error Processing {ApiRequest}", nameof(CrearDocumentoRequest));
            // Todo: Borrar documento si hay error
            return ApiResponse.CreateFailed(e.Message);
        }
        finally
        {
            if (_documentoSdkId is not 0)
                _documentoService.DesbloquearDocumento(_documentoSdkId);
        }
    }
}
