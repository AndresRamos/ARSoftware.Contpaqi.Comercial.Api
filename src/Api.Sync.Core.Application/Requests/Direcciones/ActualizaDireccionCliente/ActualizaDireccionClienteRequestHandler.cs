using Api.Core.Domain.Requests;
using Api.Sync.Core.Application.ContpaqiComercial.Interfaces;
using ARSoftware.Contpaqi.Comercial.Sdk.Abstractions.Enums;
using ARSoftware.Contpaqi.Comercial.Sdk.DatosAbstractos;
using ARSoftware.Contpaqi.Comercial.Sdk.Extras.Extensions;
using ARSoftware.Contpaqi.Comercial.Sdk.Extras.Interfaces;
using MediatR;

namespace Api.Sync.Core.Application.Requests.Direcciones.ActualizaDireccionCliente;

public sealed class
    ActualizaDireccionClienteRequestHandler : IRequestHandler<ActualizaDireccionClienteRequest, ActualizaDireccionClienteResponse>
{
    private readonly IDireccionRepository _direccionRepository;
    private readonly IDireccionService _direccionService;

    public ActualizaDireccionClienteRequestHandler(IDireccionService direccionService, IDireccionRepository direccionRepository)
    {
        _direccionService = direccionService;
        _direccionRepository = direccionRepository;
    }

    public Task<ActualizaDireccionClienteResponse> Handle(ActualizaDireccionClienteRequest request, CancellationToken cancellationToken)
    {
        tDireccion direccion = request.Model.Direccion.ToSdkDireccion();
        direccion.cCodCteProv = request.Model.CodigoCliente;

        //Todo: Cuando se corriga el SDK se debe de eliminar esta linea
        // Esto es necesario por que el SDK tiene un erro que no asigna el valor correcto al tipo de direccion
        direccion.cTipoDireccion = request.Model.Direccion.Tipo == TipoDireccion.Fiscal ? 1 : 2;

        _direccionService.Actualizar(direccion);

        Direccion? direccionActualizada =
            _direccionRepository.BuscarDireccionPorCliente(request.Model.CodigoCliente, request.Model.Direccion.Tipo);

        return Task.FromResult(new ActualizaDireccionClienteResponse(new ActualizaDireccionClienteResponseModel(direccionActualizada!)));
    }
}
