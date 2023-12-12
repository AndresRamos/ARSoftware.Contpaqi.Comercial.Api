using Api.Core.Domain.Requests.Direcciones;
using Api.Sync.Core.Application.ContpaqiComercial.Interfaces;
using ARSoftware.Contpaqi.Comercial.Sdk.Extras.Extensions;
using ARSoftware.Contpaqi.Comercial.Sdk.Extras.Interfaces;
using MediatR;

namespace Api.Sync.Core.Application.Requests.Direcciones.CrearDireccionCliente;

public sealed class CrearDireccionClienteRequestHandler : IRequestHandler<CrearDireccionClienteRequest, CrearDireccionClienteResponse>
{
    private readonly IDireccionRepository _direccionRepository;
    private readonly IDireccionService _direccionService;
    private readonly IContpaqiSdk _sdk;

    public CrearDireccionClienteRequestHandler(IDireccionService direccionService, IDireccionRepository direccionRepository,
        IContpaqiSdk sdk)
    {
        _direccionService = direccionService;
        _direccionRepository = direccionRepository;
        _sdk = sdk;
    }

    public Task<CrearDireccionClienteResponse> Handle(CrearDireccionClienteRequest request, CancellationToken cancellationToken)
    {
        // Todo: el buscar el cliente antes de crear la dirección es un parche para que no falle el sdk
        _sdk.fBuscaCteProv(request.Model.CodigoCliente).ToResultadoSdk(_sdk).ThrowIfError();

        // Todo: voler a validar en la siguiente version del SDK que se puedan crear direcciones de envio

        int direccionId = _direccionService.Crear(request.Model.CodigoCliente, request.Model.Direccion);

        Direccion direccion = _direccionRepository.BuscarDireccionPorId(direccionId) ?? throw new InvalidOperationException();

        return Task.FromResult(CrearDireccionClienteResponse.CreateInstance(direccion));
    }
}
