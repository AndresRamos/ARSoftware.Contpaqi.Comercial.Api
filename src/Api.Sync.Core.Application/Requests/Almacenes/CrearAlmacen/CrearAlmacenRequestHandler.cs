﻿using Api.Core.Domain.Common;
using Api.Core.Domain.Requests;
using Api.Sync.Core.Application.ContpaqiComercial.Interfaces;
using ARSoftware.Contpaqi.Comercial.Sdk.Extras.Extensions;
using ARSoftware.Contpaqi.Comercial.Sdk.Extras.Interfaces;
using ARSoftware.Contpaqi.Comercial.Sql.Models.Empresa;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Api.Sync.Core.Application.Requests.Almacenes.CrearAlmacen;

public sealed class CrearAlmacenRequestHandler : IRequestHandler<CrearAlmacenRequest, ApiResponse>
{
    private readonly IAlmacenRepository _almacenRepository;
    private readonly IAlmacenService _almacenService;
    private readonly ILogger _logger;

    public CrearAlmacenRequestHandler(ILogger<CrearAlmacenRequestHandler> logger, IAlmacenRepository almacenRepository,
        IAlmacenService almacenService)
    {
        _logger = logger;
        _almacenRepository = almacenRepository;
        _almacenService = almacenService;
    }

    public async Task<ApiResponse> Handle(CrearAlmacenRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var datosAlmacen = new Dictionary<string, string>(request.Model.Almacen.DatosExtra);

            datosAlmacen.TryAdd(nameof(admAlmacenes.CCODIGOALMACEN), request.Model.Almacen.Codigo);

            datosAlmacen.TryAdd(nameof(admAlmacenes.CNOMBREALMACEN), request.Model.Almacen.Nombre);

            datosAlmacen.TryAdd(nameof(admAlmacenes.CFECHAALTAALMACEN), DateTime.Today.ToSdkFecha());

            int almacenId = _almacenService.Crear(datosAlmacen);

            return ApiResponse.CreateSuccessfull<CrearAlmacenResponse, CrearAlmacenResponseModel>(new CrearAlmacenResponseModel
            {
                Almacen = (await _almacenRepository.BuscarPorIdAsync(almacenId, request.Options, cancellationToken))!
            });
        }
        catch (Exception e)
        {
            _logger.LogError("Error al crear el almacen.");
            return ApiResponse.CreateFailed(e.Message);
        }
    }
}