using Api.Core.Domain.Common;
using Api.Core.Domain.Factories;
using Api.Core.Domain.Models;
using Api.Core.Domain.Requests;
using Api.Sync.Core.Application.ContpaqiComercial.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Api.Sync.Core.Application.Documentos;

public sealed record BuscarDocumentosRequestHandler : IRequestHandler<BuscarDocumentosRequest, ApiResponseBase>
{
    private readonly IDocumentoRepository _documentoRepository;
    private readonly ILogger _logger;

    public BuscarDocumentosRequestHandler(IDocumentoRepository documentoRepository, ILogger<BuscarDocumentosRequestHandler> logger)
    {
        _documentoRepository = documentoRepository;
        _logger = logger;
    }

    public async Task<ApiResponseBase> Handle(BuscarDocumentosRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var documentos = new List<Documento>();

            //if (!string.IsNullOrWhiteSpace(request.Model.SqlQuery))
            //    documentos.AddRange(
            //        await _documentoRepository.BuscarPorSqlQueryAsync(request.Model.SqlQuery, request.Options, cancellationToken));

            documentos.AddRange(await _documentoRepository.BuscarPorRequestModelAsync(request.Model, request.Options, cancellationToken));

            return ApiResponseFactory.CreateSuccessfull<BuscarDocumentosResponse, BuscarDocumentosResponseModel>(request.Id,
                new BuscarDocumentosResponseModel { Documentos = documentos });
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error al buscar los conceptos.");
            return ApiResponseFactory.CreateFailed<BuscarConceptosResponse>(request.Id, e.Message);
        }
    }
}
