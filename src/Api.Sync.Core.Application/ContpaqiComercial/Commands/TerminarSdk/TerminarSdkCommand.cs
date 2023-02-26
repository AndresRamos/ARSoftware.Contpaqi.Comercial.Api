using ARSoftware.Contpaqi.Comercial.Sdk.Extras.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Api.Sync.Core.Application.ContpaqiComercial.Commands.TerminarSdk;

public sealed record TerminarSdkCommand : IRequest;

public sealed class TerminarSdkCommandHandler : IRequestHandler<TerminarSdkCommand>
{
    private readonly ILogger _logger;
    private readonly IComercialSdkSesionService _sdkSesionService;

    public TerminarSdkCommandHandler(IComercialSdkSesionService sdkSesionService, ILogger<TerminarSdkCommandHandler> logger)
    {
        _sdkSesionService = sdkSesionService;
        _logger = logger;
    }

    public Task Handle(TerminarSdkCommand request, CancellationToken cancellationToken)
    {
        _logger.LogDebug("Current directory: {CurrentDirectory}", Directory.GetCurrentDirectory());
        if (_sdkSesionService.IsSdkInicializado)
            _sdkSesionService.TerminarSesionSdk();

        _logger.LogDebug("SDK terminado. {@ComercialSdkSesionService}", _sdkSesionService);
        _logger.LogDebug("Current directory: {CurrentDirectory}", Directory.GetCurrentDirectory());

        return Task.CompletedTask;
    }
}
