using Api.Core.Domain.Requests;
using Api.Sync.Core.Application.ContpaqiComercial.Interfaces;
using ARSoftware.Contpaqi.Comercial.Sdk.DatosAbstractos;
using ARSoftware.Contpaqi.Comercial.Sdk.Extras.Extensions;
using ARSoftware.Contpaqi.Comercial.Sdk.Extras.Interfaces;
using MediatR;

namespace Api.Sync.Core.Application.Requests.UnidadesMedida.ActualizarUnidadMedida;

public class ActualizarUnidadMedidaRequestHandler : IRequestHandler<ActualizarUnidadMedidaRequest, ActualizarUnidadMedidaResponse>
{
    private readonly IUnidadMedidaRepository _unidadMedidaRepository;
    private readonly IUnidadMedidaService _unidadMedidaService;

    public ActualizarUnidadMedidaRequestHandler(IUnidadMedidaService unidadMedidaService, IUnidadMedidaRepository unidadMedidaRepository)
    {
        _unidadMedidaService = unidadMedidaService;
        _unidadMedidaRepository = unidadMedidaRepository;
    }

    public Task<ActualizarUnidadMedidaResponse> Handle(ActualizarUnidadMedidaRequest request, CancellationToken cancellationToken)
    {
        tUnidad unidadMedidaSdk = request.Model.UnidadMedida.ToSdkUnidad();

        _unidadMedidaService.Actualizar(request.Model.Nombre, unidadMedidaSdk);

        UnidadMedida unidadMedida = _unidadMedidaRepository.BuscarPorNombre(request.Model.Nombre) ?? throw new InvalidOperationException();

        return Task.FromResult(ActualizarUnidadMedidaResponse.CreateInstance(unidadMedida));
    }
}
