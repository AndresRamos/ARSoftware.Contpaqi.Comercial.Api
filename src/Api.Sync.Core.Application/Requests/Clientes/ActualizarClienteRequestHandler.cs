using Api.Core.Domain.Common;
using Api.Core.Domain.Requests;
using Api.Sync.Core.Application.ContpaqiComercial.Interfaces;
using ARSoftware.Contpaqi.Comercial.Sdk.Extras.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Api.Sync.Core.Application.Clientes;

public sealed class ActualizarClienteRequestHandler : IRequestHandler<ActualizarClienteRequest, ApiResponse>
{
    private readonly IClienteProveedorService _clienteProveedorService;
    private readonly IClienteRepository _clienteRepository;
    private readonly ILogger _logger;

    public ActualizarClienteRequestHandler(IClienteProveedorService clienteProveedorService, IClienteRepository clienteRepository,
        ILogger<ActualizarClienteRequestHandler> logger)
    {
        _clienteProveedorService = clienteProveedorService;
        _clienteRepository = clienteRepository;
        _logger = logger;
    }

    public async Task<ApiResponse> Handle(ActualizarClienteRequest request, CancellationToken cancellationToken)
    {
        try
        {
            _clienteProveedorService.Actualizar(request.Model.CodigoCliente, request.Model.DatosCliente);

            return ApiResponse.CreateSuccessfull<ActualizarClienteResponse, ActualizarClienteResponseModel>(
                new ActualizarClienteResponseModel
                {
                    Cliente = (await _clienteRepository.BuscarPorCodigoAsync(request.Model.CodigoCliente, request.Options,
                        cancellationToken))!
                });
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error al actualizar el cliente.");
            return ApiResponse.CreateFailed(e.Message);
        }
    }
}
