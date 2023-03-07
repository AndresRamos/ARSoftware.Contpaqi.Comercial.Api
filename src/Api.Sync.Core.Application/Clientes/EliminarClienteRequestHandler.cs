using Api.Core.Domain.Common;
using Api.Core.Domain.Factories;
using Api.Core.Domain.Requests;
using ARSoftware.Contpaqi.Comercial.Sdk.Extras.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Api.Sync.Core.Application.Clientes;

public sealed class EliminarClienteRequestHandler : IRequestHandler<EliminarClienteRequest, ApiResponseBase>
{
    private readonly IClienteProveedorService _clienteProveedorService;
    private readonly ILogger _logger;

    public EliminarClienteRequestHandler(IClienteProveedorService clienteProveedorService, ILogger<EliminarClienteRequestHandler> logger)
    {
        _clienteProveedorService = clienteProveedorService;
        _logger = logger;
    }

    public Task<ApiResponseBase> Handle(EliminarClienteRequest request, CancellationToken cancellationToken)
    {
        try
        {
            _clienteProveedorService.Eliminar(request.Model.CodigoCliente);

            return Task.FromResult<ApiResponseBase>(
                ApiResponseFactory.CreateSuccessfull<EliminarClienteResponse, EliminarClienteResponseModel>(request.Id,
                    new EliminarClienteResponseModel()));
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error al eliminar el cliente.");
            return Task.FromResult<ApiResponseBase>(ApiResponseFactory.CreateFailed<EliminarClienteResponse>(request.Id, e.Message));
        }
    }
}
