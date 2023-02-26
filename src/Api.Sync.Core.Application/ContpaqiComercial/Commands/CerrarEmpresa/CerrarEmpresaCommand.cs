using ARSoftware.Contpaqi.Comercial.Sdk.Extras.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Api.Sync.Core.Application.ContpaqiComercial.Commands.CerrarEmpresa;

public sealed record CerrarEmpresaCommand : IRequest;

public sealed class CerrarEmpresaCommandHandler : IRequestHandler<CerrarEmpresaCommand>
{
    private readonly ILogger _logger;
    private readonly IComercialSdkSesionService _sdkSesionService;

    public CerrarEmpresaCommandHandler(IComercialSdkSesionService sdkSesionService, ILogger<CerrarEmpresaCommandHandler> logger)
    {
        _sdkSesionService = sdkSesionService;
        _logger = logger;
    }

    public Task Handle(CerrarEmpresaCommand request, CancellationToken cancellationToken)
    {
        if (_sdkSesionService.IsEmpresaAbierta)
            _sdkSesionService.CerrarEmpresa();

        _logger.LogDebug("Empresa cerrada. {@ComercialSdkSesionService}", _sdkSesionService);

        return Task.CompletedTask;
    }
}
