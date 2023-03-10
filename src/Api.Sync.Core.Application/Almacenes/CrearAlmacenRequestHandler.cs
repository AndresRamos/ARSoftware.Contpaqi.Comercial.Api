using Api.Core.Domain.Common;
using Api.Core.Domain.Factories;
using Api.Core.Domain.Requests;
using Api.Sync.Core.Application.ContpaqiComercial.Interfaces;
using ARSoftware.Contpaqi.Comercial.Sdk.Extras.Interfaces;
using ARSoftware.Contpaqi.Comercial.Sql.Models.Empresa;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Api.Sync.Core.Application.Almacenes;

public sealed class CrearAlmacenRequestHandler : IRequestHandler<CrearAlmacenRequest, ApiResponseBase>
{
    private readonly IAlmacenRepository _almacenRepository;
    private readonly IAlmacenService _almacenService;
    private readonly ILogger _logger;

    public CrearAlmacenRequestHandler(ILogger<CrearAlmacenRequestHandler> logger,
                                      IAlmacenRepository almacenRepository,
                                      IAlmacenService almacenService)
    {
        _logger = logger;
        _almacenRepository = almacenRepository;
        _almacenService = almacenService;
    }

    public async Task<ApiResponseBase> Handle(CrearAlmacenRequest request, CancellationToken cancellationToken)
    {
        try
        {
            int almacenId = _almacenService.Crear(new Dictionary<string, string>(request.Model.Almacen.DatosExtra)
            {
                { nameof(admAlmacenes.CCODIGOALMACEN), request.Model.Almacen.Codigo },
                { nameof(admAlmacenes.CNOMBREALMACEN), request.Model.Almacen.Nombre }
            });

            return ApiResponseFactory.CreateSuccessfull<CrearAlmacenResponse, CrearAlmacenResponseModel>(request.Id,
                new CrearAlmacenResponseModel
                {
                    Almacen = (await _almacenRepository.BuscarPorIdAsync(almacenId, request.Options, cancellationToken))!
                });
        }
        catch (Exception e)
        {
            _logger.LogError("Error al crear el almacen.");
            return ApiResponseFactory.CreateFailed<CrearAlmacenResponse>(request.Id, e.Message);
        }
    }
}
