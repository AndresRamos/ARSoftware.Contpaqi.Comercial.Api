using Api.Core.Domain.Requests.Direcciones;
using Api.Sync.Core.Application.ContpaqiComercial.Interfaces;
using ARSoftware.Contpaqi.Comercial.Sdk.Extras.Interfaces;
using MediatR;

namespace Api.Sync.Core.Application.Requests.Direcciones.CrearDireccionCliente;

public sealed class CrearDireccionClienteRequestHandler : IRequestHandler<CreaDireccionClienteRequest, CreaDireccionClienteResponse>
{
    private readonly IDireccionRepository _direccionRepository;
    private readonly IDireccionService _direccionService;

    public CrearDireccionClienteRequestHandler(IDireccionService direccionService, IDireccionRepository direccionRepository)
    {
        _direccionService = direccionService;
        _direccionRepository = direccionRepository;
    }

    public Task<CreaDireccionClienteResponse> Handle(CreaDireccionClienteRequest request, CancellationToken cancellationToken)
    {
        int direccionId = _direccionService.Crear(request.Model.CodigoCliente, request.Model.Direccion);

        Direccion direccion = _direccionRepository.BuscarDireccionPorId(direccionId) ?? throw new InvalidOperationException();

        return Task.FromResult(CreaDireccionClienteResponse.CreateInstance(direccion));
    }
}
