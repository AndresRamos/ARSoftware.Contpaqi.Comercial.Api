using Api.Core.Domain.Common;
using Api.Core.Domain.Models;
using Api.Core.Domain.Requests;
using Api.Sync.Core.Application.ContpaqiComercial.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Api.Sync.Core.Application.Requests.Clientes;

public sealed class BuscarClientesRequestHandler : IRequestHandler<BuscarClientesRequest, ApiResponse>
{
    private readonly IClienteRepository _clienteRepository;
    private readonly ILogger _logger;

    public BuscarClientesRequestHandler(IClienteRepository clienteRepository, ILogger<BuscarClientesRequestHandler> logger)
    {
        _clienteRepository = clienteRepository;
        _logger = logger;
    }

    public async Task<ApiResponse> Handle(BuscarClientesRequest request, CancellationToken cancellationToken)
    {
        try
        {
            List<Cliente> clientes = (await _clienteRepository.BuscarPorRequestModel(request.Model, request.Options, cancellationToken))
                .ToList();

            return ApiResponse.CreateSuccessfull<BuscarClientesResponse, BuscarClientesResponseModel>(
                new BuscarClientesResponseModel { Clientes = clientes });
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error al buscar clientes.");
            return ApiResponse.CreateFailed(e.Message);
        }
    }
}
