using Api.Core.Domain.Requests;
using Api.Sync.Core.Application.ContpaqiComercial.Interfaces;
using MediatR;

namespace Api.Sync.Core.Application.Requests.Direcciones.BuscarDireccionCliente;

public sealed class BuscarDireccionClienteRequestHandler : IRequestHandler<BuscarDireccionClienteRequest, BuscarDireccionClienteResponse>
{
    private readonly IDireccionRepository _direccionRepository;

    public BuscarDireccionClienteRequestHandler(IDireccionRepository direccionRepository)
    {
        _direccionRepository = direccionRepository;
    }

    public async Task<BuscarDireccionClienteResponse> Handle(BuscarDireccionClienteRequest request, CancellationToken cancellationToken)
    {
        Direccion? direccion = _direccionRepository.BuscarDireccionPorCliente(request.Model.CodigoCliente, request.Model.Tipo);

        return BuscarDireccionClienteResponse.CreateInstance(direccion);
    }
}
