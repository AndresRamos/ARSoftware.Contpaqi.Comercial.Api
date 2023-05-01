using Api.Core.Domain.Models;
using Api.Core.Domain.Requests;
using Api.Sync.Core.Application.ContpaqiComercial.Interfaces;
using MediatR;

namespace Api.Sync.Core.Application.Requests.Clientes.BuscarClientes;

public sealed class BuscarClientesRequestHandler : IRequestHandler<BuscarClientesRequest, BuscarClientesResponse>
{
    private readonly IClienteRepository _clienteRepository;

    public BuscarClientesRequestHandler(IClienteRepository clienteRepository)
    {
        _clienteRepository = clienteRepository;
    }

    public async Task<BuscarClientesResponse> Handle(BuscarClientesRequest request, CancellationToken cancellationToken)
    {
        List<Cliente> clientes =
            (await _clienteRepository.BuscarPorRequestModel(request.Model, request.Options, cancellationToken)).ToList();

        return BuscarClientesResponse.CreateInstance(clientes);
    }
}
