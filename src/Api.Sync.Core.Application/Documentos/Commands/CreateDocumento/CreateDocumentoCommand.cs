using Api.Core.Domain.Models;
using Api.Core.Domain.Requests;
using ARSoftware.Contpaqi.Comercial.Sdk.DatosAbstractos;
using ARSoftware.Contpaqi.Comercial.Sdk.Extras.Extensions;
using ARSoftware.Contpaqi.Comercial.Sdk.Extras.Helpers;
using ARSoftware.Contpaqi.Comercial.Sdk.Extras.Interfaces;
using ARSoftware.Contpaqi.Comercial.Sql.Models.Empresa;
using AutoMapper;
using MediatR;

namespace Api.Sync.Core.Application.Documentos.Commands.CreateDocumento;

public sealed record CreateDocumentoCommand(Documento Documento, CreateDocumentoOptions Options) : IRequest<int>;

public sealed class CreateDocumentoCommandHandler : IRequestHandler<CreateDocumentoCommand, int>
{
    private readonly IDocumentoService _documentoService;
    private readonly IMapper _mapper;
    private readonly IMovimientoService _movimientoService;

    public CreateDocumentoCommandHandler(IDocumentoService documentoService, IMapper mapper, IMovimientoService movimientoService)
    {
        _documentoService = documentoService;
        _mapper = mapper;
        _movimientoService = movimientoService;
    }

    public Task<int> Handle(CreateDocumentoCommand request, CancellationToken cancellationToken)
    {
        var documentoSdk = _mapper.Map<tDocumento>(request.Documento);
        if (request.Options.UsarFechaDelDia)
            documentoSdk.aFecha = DateTime.Today.ToSdkFecha();

        if (request.Options.BuscarSiguienteFolio)
        {
            tLlaveDoc siguienteFolio = _documentoService.BuscarSiguienteSerieYFolio(documentoSdk.aCodConcepto);
            documentoSdk.aSerie = siguienteFolio.aSerie;
            documentoSdk.aFolio = siguienteFolio.aFolio;
        }

        int documentoSdkId = _documentoService.Crear(documentoSdk);

        var datosDocumento = new Dictionary<string, string>(request.Documento.DatosExtra)
        {
            { nameof(admDocumentos.COBSERVACIONES), request.Documento.Observaciones },
            { nameof(admDocumentos.CMETODOPAG), request.Documento.FormaPago.Clave },
            { nameof(admDocumentos.CCANTPARCI), MetodoPagoHelper.ConvertToSdkValue(request.Documento.MetodoPago).ToString() }
        };

        _documentoService.Actualizar(documentoSdkId, datosDocumento);

        foreach (Movimiento movimiento in request.Documento.Movimientos)
        {
            var movimientoSdk = _mapper.Map<tMovimiento>(movimiento);
            int movimientoSdkId = _movimientoService.Crear(documentoSdkId, movimientoSdk);
            var datosMovimiento = new Dictionary<string, string>(movimiento.DatosExtra)
            {
                { nameof(admMovimientos.COBSERVAMOV), movimiento.Observaciones }
            };
            // todo: modificar porcentages e importes de impuestos y descuentos dependiendo de la configuracion del concepto
            _movimientoService.Actualizar(movimientoSdkId, datosMovimiento);
        }

        return Task.FromResult(documentoSdkId);
    }
}
