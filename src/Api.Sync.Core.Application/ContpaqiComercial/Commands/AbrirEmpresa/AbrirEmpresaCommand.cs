using Api.Sync.Core.Application.Common.Models;
using ARSoftware.Contpaqi.Comercial.Sdk.Extras.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Api.Sync.Core.Application.ContpaqiComercial.Commands.AbrirEmpresa;

public sealed record AbrirEmpresaCommand : IRequest;

public sealed class AbrirEmpresaCommandHandler : IRequestHandler<AbrirEmpresaCommand>
{
    private static string _empresaRfc = "";
    private readonly ContpaqiComercialConfig _contpaqiComercialConfig;
    private readonly ILogger _logger;
    private readonly IComercialSdkSesionService _sdkSesionService;

    public AbrirEmpresaCommandHandler(IComercialSdkSesionService sdkSesionService,
        IOptions<ContpaqiComercialConfig> contpaqiComercialConfigOptions, ILogger<AbrirEmpresaCommand> logger)
    {
        _sdkSesionService = sdkSesionService;
        _logger = logger;
        _contpaqiComercialConfig = contpaqiComercialConfigOptions.Value;
    }

    public Task Handle(AbrirEmpresaCommand request, CancellationToken cancellationToken)
    {
        if (_sdkSesionService.IsEmpresaAbierta && _empresaRfc != _contpaqiComercialConfig.Empresa.Rfc)
        {
            _logger.LogDebug("Ceranndo empresa {EmpresaRfc}", _empresaRfc);
            _sdkSesionService.CerrarEmpresa();
        }

        _logger.LogInformation("Abriendo empresa. {0} - {1}", _contpaqiComercialConfig.Empresa.Nombre,
            _contpaqiComercialConfig.Empresa.Rfc);

        if (!_sdkSesionService.IsEmpresaAbierta)
        {
            _sdkSesionService.AbrirEmpresa(_contpaqiComercialConfig.Empresa.Ruta);
            _empresaRfc = _contpaqiComercialConfig.Empresa.Rfc;
            _logger.LogDebug("Empresa abierta. {@ComercialSdkSesionService}", _sdkSesionService);
        }

        return Task.CompletedTask;
    }
}
