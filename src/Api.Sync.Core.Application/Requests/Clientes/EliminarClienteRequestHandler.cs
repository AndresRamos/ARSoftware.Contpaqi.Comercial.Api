using Api.Core.Domain.Common;
using Api.Core.Domain.Requests;
using ARSoftware.Contpaqi.Comercial.Sdk.Extras.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Api.Sync.Core.Application.Requests.Clientes;

public sealed class EliminarClienteRequestHandler : IRequestHandler<EliminarClienteRequest, ApiResponse>
{
    private readonly IClienteProveedorService _clienteProveedorService;
    private readonly ILogger _logger;

    public EliminarClienteRequestHandler(IClienteProveedorService clienteProveedorService, ILogger<EliminarClienteRequestHandler> logger)
    {
        _clienteProveedorService = clienteProveedorService;
        _logger = logger;
    }

    public Task<ApiResponse> Handle(EliminarClienteRequest request, CancellationToken cancellationToken)
    {
        try
        {
            _clienteProveedorService.Eliminar(request.Model.CodigoCliente);

            return Task.FromResult(
                ApiResponse.CreateSuccessfull<EliminarClienteResponse, EliminarClienteResponseModel>(new EliminarClienteResponseModel()));
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error al eliminar el cliente.");
            return Task.FromResult(ApiResponse.CreateFailed(e.Message));
        }
    }
}
