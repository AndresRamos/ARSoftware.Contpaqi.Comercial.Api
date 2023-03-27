using Api.Core.Domain.Common;
using Api.Core.Domain.Factories;
using Api.Core.Domain.Models;
using Api.Core.Domain.Requests;
using Api.Sync.Core.Application.ContpaqiComercial.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Api.Sync.Core.Application.Conceptos;

public sealed class BuscarConceptosRequestHandler : IRequestHandler<BuscarConceptosRequest, ApiResponseBase>
{
    private readonly IConceptoRepository _conceptoRepository;
    private readonly ILogger _logger;

    public BuscarConceptosRequestHandler(IConceptoRepository conceptoRepository, ILogger<BuscarConceptosRequestHandler> logger)
    {
        _conceptoRepository = conceptoRepository;
        _logger = logger;
    }

    public async Task<ApiResponseBase> Handle(BuscarConceptosRequest request, CancellationToken cancellationToken)
    {
        try
        {
            List<Concepto> conceptos =
                (await _conceptoRepository.BuscarPorRequstModelAsync(request.Model, request.Options, cancellationToken)).ToList();

            return ApiResponseFactory.CreateSuccessfull<BuscarConceptosResponse, BuscarConceptosResponseModel>(request.Id,
                new BuscarConceptosResponseModel { Conceptos = conceptos });
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error al buscar los conceptos.");
            return ApiResponseFactory.CreateFailed<BuscarConceptosResponse>(request.Id, e.Message);
        }
    }
}
