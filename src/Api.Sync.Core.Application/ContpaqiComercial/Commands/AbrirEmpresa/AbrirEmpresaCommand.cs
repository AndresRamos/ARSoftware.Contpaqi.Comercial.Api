﻿using Api.Sync.Core.Application.Common.Models;
using ARSoftware.Contpaqi.Comercial.Sdk.Extras.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Api.Sync.Core.Application.ContpaqiComercial.Commands.AbrirEmpresa;

public sealed record AbrirEmpresaCommand : IRequest;

public sealed class AbrirEmpresaCommandHandler : IRequestHandler<AbrirEmpresaCommand>
{
    private readonly ContpaqiComercialConfig _contpaqiComercialConfig;
    private readonly ILogger _logger;
    private readonly IComercialSdkSesionService _sdkSesionService;

    public AbrirEmpresaCommandHandler(IComercialSdkSesionService sdkSesionService,
                                      IOptions<ContpaqiComercialConfig> contpaqiComercialConfigOptions,
                                      ILogger<AbrirEmpresaCommand> logger)
    {
        _sdkSesionService = sdkSesionService;
        _logger = logger;
        _contpaqiComercialConfig = contpaqiComercialConfigOptions.Value;
    }

    public Task Handle(AbrirEmpresaCommand request, CancellationToken cancellationToken)
    {
        if (!_sdkSesionService.IsEmpresaAbierta)
            _sdkSesionService.AbrirEmpresa(_contpaqiComercialConfig.Empresa.Ruta);

        _logger.LogDebug("Empresa abierta. {@ComercialSdkSesionService}", _sdkSesionService);

        return Task.CompletedTask;
    }
}
