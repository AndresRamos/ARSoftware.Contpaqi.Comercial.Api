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
            var conceptos = new List<Concepto>();

            if (request.Model.Id is not null)
            {
                Concepto? concepto = await _conceptoRepository.BuscarPorIdAsync(request.Model.Id.Value, request.Options, cancellationToken);
                if (concepto is not null)
                    conceptos.Add(concepto);
            }
            else if (request.Model.Codigo is not null)
            {
                Concepto? concepto =
                    await _conceptoRepository.BuscarPorCodigoAsync(request.Model.Codigo, request.Options, cancellationToken);
                if (concepto is not null)
                    conceptos.Add(concepto);
            }
            else
            {
                conceptos.AddRange(await _conceptoRepository.BuscarTodoAsync(request.Options, cancellationToken));
            }

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
