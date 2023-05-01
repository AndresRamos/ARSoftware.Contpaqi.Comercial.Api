using Api.Core.Domain.Models;
using Api.Core.Domain.Requests;
using Api.Sync.Core.Application.ContpaqiComercial.Interfaces;
using ARSoftware.Contpaqi.Comercial.Sdk.Extras.Interfaces;
using MediatR;

namespace Api.Sync.Core.Application.Requests.Clientes.ActualizarCliente;

public sealed class ActualizarClienteRequestHandler : IRequestHandler<ActualizarClienteRequest, ActualizarClienteResponse>
{
    private readonly IClienteProveedorService _clienteProveedorService;
    private readonly IClienteRepository _clienteRepository;

    public ActualizarClienteRequestHandler(IClienteProveedorService clienteProveedorService, IClienteRepository clienteRepository)
    {
        _clienteProveedorService = clienteProveedorService;
        _clienteRepository = clienteRepository;
    }

    public async Task<ActualizarClienteResponse> Handle(ActualizarClienteRequest request, CancellationToken cancellationToken)
    {
        _clienteProveedorService.Actualizar(request.Model.CodigoCliente, request.Model.DatosCliente);

        Cliente cliente = await _clienteRepository.BuscarPorCodigoAsync(request.Model.CodigoCliente, request.Options, cancellationToken) ??
                          throw new InvalidOperationException();

        return ActualizarClienteResponse.CreateInstance(cliente);
    }
}
