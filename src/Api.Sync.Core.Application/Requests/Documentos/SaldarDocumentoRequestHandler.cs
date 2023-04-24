using Api.Core.Domain.Common;
using Api.Core.Domain.Requests;
using Api.Sync.Core.Application.ContpaqiComercial.Interfaces;
using ARSoftware.Contpaqi.Comercial.Sdk.DatosAbstractos;
using ARSoftware.Contpaqi.Comercial.Sdk.Extras.Interfaces;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Api.Sync.Core.Application.Requests.Documentos;

public sealed class SaldarDocumentoRequestHandler : IRequestHandler<SaldarDocumentoRequest, ApiResponse>
{
    private readonly IDocumentoRepository _documentoRepository;
    private readonly IDocumentoService _documentoService;
    private readonly ILogger _logger;
    private readonly IMapper _mapper;

    public SaldarDocumentoRequestHandler(IDocumentoService documentoService, IDocumentoRepository documentoRepository,
        ILogger<SaldarDocumentoRequestHandler> logger, IMapper mapper)
    {
        _documentoService = documentoService;
        _documentoRepository = documentoRepository;
        _logger = logger;
        _mapper = mapper;
    }

    public async Task<ApiResponse> Handle(SaldarDocumentoRequest request, CancellationToken cancellationToken)
    {
        try
        {
            _documentoService.SaldarDocumento(_mapper.Map<tLlaveDoc>(request.Model.DocumentoAPagar),
                _mapper.Map<tLlaveDoc>(request.Model.DocumentoPago), request.Model.Fecha, (double)request.Model.Importe);

            return ApiResponse.CreateSuccessfull<SaldarDocumentoResponse, SaldarDocumentoResponseModel>(new SaldarDocumentoResponseModel
            {
                DocumentoPagar =
                    (await _documentoRepository.BuscarPorLlaveAsync(request.Model.DocumentoAPagar, request.Options,
                        cancellationToken))!,
                DocumentoPago =
                    (await _documentoRepository.BuscarPorLlaveAsync(request.Model.DocumentoPago, request.Options, cancellationToken))!
            });
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error al saldar el documento.");
            return ApiResponse.CreateFailed(e.Message);
        }
    }
}
