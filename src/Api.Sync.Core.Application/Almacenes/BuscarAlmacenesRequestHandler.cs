using Api.Core.Domain.Common;
using Api.Core.Domain.Factories;
using Api.Core.Domain.Models;
using Api.Core.Domain.Requests;
using Api.Sync.Core.Application.ContpaqiComercial.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Api.Sync.Core.Application.Almacenes;

public sealed class BuscarAlmacenesRequestHandler : IRequestHandler<BuscarAlmacenesRequest, ApiResponseBase>
{
    private readonly IAlmacenRepository _almacenRepository;
    private readonly ILogger _logger;

    public BuscarAlmacenesRequestHandler(IAlmacenRepository almacenRepository, ILogger<BuscarAlmacenesRequestHandler> logger)
    {
        _almacenRepository = almacenRepository;
        _logger = logger;
    }

    public async Task<ApiResponseBase> Handle(BuscarAlmacenesRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var almacenes = new List<Almacen>();

            if (request.Model.Id is not null)
            {
                Almacen? almacen = await _almacenRepository.BuscarPorIdAsync(request.Model.Id.Value, request.Options, cancellationToken);
                if (almacen is not null)
                    almacenes.Add(almacen);
            }
            else if (request.Model.Codigo is not null)
            {
                Almacen? almacen = await _almacenRepository.BuscarPorCodigoAsync(request.Model.Codigo, request.Options, cancellationToken);
                if (almacen is not null)
                    almacenes.Add(almacen);
            }
            else
            {
                almacenes.AddRange(await _almacenRepository.BuscarTodoAsync(request.Options, cancellationToken));
            }

            return ApiResponseFactory.CreateSuccessfull<BuscarAlmacenesResponse, BuscarAlmacenesResponseModel>(request.Id,
                new BuscarAlmacenesResponseModel { Almacenes = almacenes });
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error al buscar almacenes.");
            return ApiResponseFactory.CreateFailed<BuscarAlmacenesResponse>(request.Id, e.Message);
        }
    }
}
