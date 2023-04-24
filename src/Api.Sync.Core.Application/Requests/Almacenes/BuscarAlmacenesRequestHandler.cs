using Api.Core.Domain.Common;
using Api.Core.Domain.Models;
using Api.Core.Domain.Requests;
using Api.Sync.Core.Application.ContpaqiComercial.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Api.Sync.Core.Application.Almacenes;

public sealed class BuscarAlmacenesRequestHandler : IRequestHandler<BuscarAlmacenesRequest, ApiResponse>
{
    private readonly IAlmacenRepository _almacenRepository;
    private readonly ILogger _logger;

    public BuscarAlmacenesRequestHandler(IAlmacenRepository almacenRepository, ILogger<BuscarAlmacenesRequestHandler> logger)
    {
        _almacenRepository = almacenRepository;
        _logger = logger;
    }

    public async Task<ApiResponse> Handle(BuscarAlmacenesRequest request, CancellationToken cancellationToken)
    {
        try
        {
            List<Almacen> almacenes =
                (await _almacenRepository.BuscarPorRequestModelAsync(request.Model, request.Options, cancellationToken)).ToList();

            return ApiResponse.CreateSuccessfull<BuscarAlmacenesResponse, BuscarAlmacenesResponseModel>(
                new BuscarAlmacenesResponseModel { Almacenes = almacenes });
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error al buscar almacenes.");
            return ApiResponse.CreateFailed(e.Message);
        }
    }
}
