using Api.Core.Domain.Common;
using Api.Core.Domain.Requests;
using Api.Sync.Core.Application.ContpaqiComercial.Interfaces;
using ARSoftware.Contpaqi.Comercial.Sdk.Extras.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Api.Sync.Core.Application.Requests.Almacenes;

public sealed class ActualizarAlmacenRequestHandler : IRequestHandler<ActualizarAlmacenRequest, ApiResponse>
{
    private readonly IAlmacenRepository _almacenRepository;
    private readonly IAlmacenService _almacenService;
    private readonly ILogger _logger;

    public ActualizarAlmacenRequestHandler(IAlmacenService almacenService, IAlmacenRepository almacenRepository,
        ILogger<ActualizarAlmacenRequestHandler> logger)
    {
        _almacenService = almacenService;
        _almacenRepository = almacenRepository;
        _logger = logger;
    }

    public async Task<ApiResponse> Handle(ActualizarAlmacenRequest request, CancellationToken cancellationToken)
    {
        try
        {
            _almacenService.Actualizar(request.Model.CodigoAlmacen, request.Model.DatosAlmacen);

            return ApiResponse.CreateSuccessfull<ActualizarAlmacenResponse, ActualizarAlmacenResponseModel>(
                new ActualizarAlmacenResponseModel
                {
                    Almacen = (await _almacenRepository.BuscarPorCodigoAsync(request.Model.CodigoAlmacen, request.Options,
                        cancellationToken))!
                });
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error al actualizar el almacen.");
            return ApiResponse.CreateFailed(e.Message);
        }
    }
}
