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

    public Task<Unit> Handle(TerminarSdkCommand request, CancellationToken cancellationToken)
    {
        if (_sdkSesionService.IsSdkInicializado)
            _sdkSesionService.TerminarSesionSdk();

        _logger.LogInformation("SDK Terminado.");

        return Unit.Task;
    }
}
