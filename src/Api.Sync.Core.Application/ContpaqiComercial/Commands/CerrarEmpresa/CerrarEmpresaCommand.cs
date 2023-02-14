using ARSoftware.Contpaqi.Comercial.Sdk.Extras.Interfaces;
using MediatR;

namespace Api.Sync.Core.Application.ContpaqiComercial.Commands.CerrarEmpresa;

public sealed record CerrarEmpresaCommand : IRequest;

public sealed class CerrarEmpresaCommandHandler : IRequestHandler<CerrarEmpresaCommand>
{
    private readonly IComercialSdkSesionService _sdkSesionService;

    public CerrarEmpresaCommandHandler(IComercialSdkSesionService sdkSesionService)
    {
        _sdkSesionService = sdkSesionService;
    }

    public Task<Unit> Handle(CerrarEmpresaCommand request, CancellationToken cancellationToken)
    {
        if (_sdkSesionService.IsEmpresaAbierta)
            _sdkSesionService.CerrarEmpresa();

        return Unit.Task;
    }
}
