using Api.Core.Domain.Requests;
using ARSoftware.Contpaqi.Comercial.Sdk.Extras.Interfaces;
using MediatR;

namespace Api.Sync.Core.Application.Requests.Clientes.EliminarCliente;

public sealed class EliminarClienteRequestHandler : IRequestHandler<EliminarClienteRequest, EliminarClienteResponse>
{
    private readonly IClienteProveedorService _clienteProveedorService;

    public EliminarClienteRequestHandler(IClienteProveedorService clienteProveedorService)
    {
        _clienteProveedorService = clienteProveedorService;
    }

    public Task<EliminarClienteResponse> Handle(EliminarClienteRequest request, CancellationToken cancellationToken)
    {
        _clienteProveedorService.Eliminar(request.Model.CodigoCliente);

        return Task.FromResult(EliminarClienteResponse.CreateInstance());
    }
}
