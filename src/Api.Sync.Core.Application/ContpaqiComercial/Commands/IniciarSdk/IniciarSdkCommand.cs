using Api.Sync.Core.Application.Common.Models;
using ARSoftware.Contpaqi.Comercial.Sdk.Extras.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Api.Sync.Core.Application.ContpaqiComercial.Commands.IniciarSdk;

public sealed record IniciarSdkCommand : IRequest;

public sealed class IniciarSdkCommandHandler : IRequestHandler<IniciarSdkCommand>
{
    private readonly ContpaqiComercialConfig _contpaqiComercialConfig;
    private readonly ILogger _logger;
    private readonly IComercialSdkSesionService _sdkSesionService;

    public IniciarSdkCommandHandler(IComercialSdkSesionService sdkSesionService,
                                    IOptions<ContpaqiComercialConfig> contpaqiComercialConfig,
                                    ILogger<IniciarSdkCommandHandler> logger)
    {
        _sdkSesionService = sdkSesionService;
        _logger = logger;
        _contpaqiComercialConfig = contpaqiComercialConfig.Value;
    }

    public Task<Unit> Handle(IniciarSdkCommand request, CancellationToken cancellationToken)
    {
        if (!_sdkSesionService.IsSdkInicializado)
            _sdkSesionService.IniciarSesionSdk(_contpaqiComercialConfig.Usuario, _contpaqiComercialConfig.Contrasena);

        _logger.LogInformation("SDK Inicializado.");

        return Unit.Task;
    }
}
