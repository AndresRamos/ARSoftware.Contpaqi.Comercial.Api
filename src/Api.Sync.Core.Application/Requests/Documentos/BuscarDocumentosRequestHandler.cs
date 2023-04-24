using Api.Core.Domain.Common;
using Api.Core.Domain.Models;
using Api.Core.Domain.Requests;
using Api.Sync.Core.Application.ContpaqiComercial.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Api.Sync.Core.Application.Requests.Documentos;

public sealed record BuscarDocumentosRequestHandler : IRequestHandler<BuscarDocumentosRequest, ApiResponse>
{
    private readonly IDocumentoRepository _documentoRepository;
    private readonly ILogger _logger;

    public BuscarDocumentosRequestHandler(IDocumentoRepository documentoRepository, ILogger<BuscarDocumentosRequestHandler> logger)
    {
        _documentoRepository = documentoRepository;
        _logger = logger;
    }

    public async Task<ApiResponse> Handle(BuscarDocumentosRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var documentos = new List<Documento>();

            documentos.AddRange(await _documentoRepository.BuscarPorRequestModelAsync(request.Model, request.Options, cancellationToken));

            return ApiResponse.CreateSuccessfull<BuscarDocumentosResponse, BuscarDocumentosResponseModel>(
                new BuscarDocumentosResponseModel { Documentos = documentos });
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error al buscar los conceptos.");
            return ApiResponse.CreateFailed(e.Message);
        }
    }
}
