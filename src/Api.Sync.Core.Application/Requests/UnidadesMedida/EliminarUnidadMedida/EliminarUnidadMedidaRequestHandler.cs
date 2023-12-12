using Api.Core.Domain.Requests;
using ARSoftware.Contpaqi.Comercial.Sdk.Extras.Interfaces;
using MediatR;

namespace Api.Sync.Core.Application.Requests.UnidadesMedida.EliminarUnidadMedida;

public sealed class EliminarUnidadMedidaRequestHandler : IRequestHandler<EliminarUnidadMedidaRequest, EliminarUnidadMedidaResponse>
{
    private readonly IUnidadMedidaService _unidadMedidaService;

    public EliminarUnidadMedidaRequestHandler(IUnidadMedidaService unidadMedidaService)
    {
        _unidadMedidaService = unidadMedidaService;
    }

    public Task<EliminarUnidadMedidaResponse> Handle(EliminarUnidadMedidaRequest request, CancellationToken cancellationToken)
    {
        _unidadMedidaService.Eliminar(request.Model.NombreUnidad);

        return Task.FromResult(new EliminarUnidadMedidaResponse(new EliminarUnidadMedidaResponseModel()));
    }
}
