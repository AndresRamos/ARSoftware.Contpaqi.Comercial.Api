using Api.Core.Domain.Common;
using Api.Core.Domain.Factories;
using Api.Core.Domain.Models;
using Api.Core.Domain.Requests;
using Api.Sync.Core.Application.ContpaqiComercial.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Api.Sync.Core.Application.Clientes;

public sealed class BuscarClientesRequestHandler : IRequestHandler<BuscarClientesRequest, ApiResponseBase>
{
    private readonly IClienteRepository _clienteRepository;
    private readonly ILogger _logger;

    public BuscarClientesRequestHandler(IClienteRepository clienteRepository, ILogger<BuscarClientesRequestHandler> logger)
    {
        _clienteRepository = clienteRepository;
        _logger = logger;
    }

    public async Task<ApiResponseBase> Handle(BuscarClientesRequest request, CancellationToken cancellationToken)
    {
        try
        {
            List<Cliente> clientes = (await _clienteRepository.BuscarPorRequestModel(request.Model, request.Options, cancellationToken))
                .ToList();

            return ApiResponseFactory.CreateSuccessfull<BuscarClientesResponse, BuscarClientesResponseModel>(request.Id,
                new BuscarClientesResponseModel { Clientes = clientes });
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error al buscar clientes.");
            return ApiResponseFactory.CreateFailed<BuscarClientesResponse>(request.Id, e.Message);
        }
    }
}
