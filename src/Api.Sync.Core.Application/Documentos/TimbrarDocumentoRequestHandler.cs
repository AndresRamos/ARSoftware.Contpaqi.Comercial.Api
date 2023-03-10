using Api.Core.Domain.Common;
using Api.Core.Domain.Factories;
using Api.Core.Domain.Requests;
using Api.Sync.Core.Application.ContpaqiComercial.Interfaces;
using ARSoftware.Contpaqi.Comercial.Sdk.DatosAbstractos;
using ARSoftware.Contpaqi.Comercial.Sdk.Extras.Interfaces;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Api.Sync.Core.Application.Documentos;

public sealed class TimbrarDocumentoRequestHandler : IRequestHandler<TimbrarDocumentoRequest, ApiResponseBase>
{
    private readonly IDocumentoRepository _documentoRepository;
    private readonly IDocumentoService _documentoService;
    private readonly ILogger _logger;
    private readonly IMapper _mapper;

    public TimbrarDocumentoRequestHandler(IDocumentoService documentoService,
                                          IDocumentoRepository documentoRepository,
                                          ILogger<TimbrarDocumentoRequestHandler> logger,
                                          IMapper mapper)
    {
        _documentoService = documentoService;
        _documentoRepository = documentoRepository;
        _logger = logger;
        _mapper = mapper;
    }

    public async Task<ApiResponseBase> Handle(TimbrarDocumentoRequest request, CancellationToken cancellationToken)
    {
        try
        {
            string rutaArchivoAdicional = await GetRutaArchivoAdicionalAsync(request, cancellationToken);

            _documentoService.Timbrar(_mapper.Map<tLlaveDoc>(request.Model.LlaveDocumento),
                request.Model.ContrasenaCertificado,
                rutaArchivoAdicional);

            return ApiResponseFactory.CreateSuccessfull<TimbrarDocumentoResponse, TimbrarDocumentoResponseModel>(request.Id,
                new TimbrarDocumentoResponseModel
                {
                    Documento = (await _documentoRepository.BuscarPorLlaveAsync(request.Model.LlaveDocumento,
                        request.Options,
                        cancellationToken))!
                });
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error al saldar el documento.");
            return ApiResponseFactory.CreateFailed<TimbrarDocumentoResponse>(request.Id, e.Message);
        }
    }

    private async Task<string> GetRutaArchivoAdicionalAsync(TimbrarDocumentoRequest request, CancellationToken cancellationToken)
    {
        if (!request.Options.AgregarArchivo)
            return string.Empty;

        string rutaArchivoAdicional = Path.Combine(Path.GetTempPath(), request.Options.NombreArchivo);
        await File.WriteAllTextAsync(rutaArchivoAdicional, request.Options.ContenidoArchivo, cancellationToken);
        return rutaArchivoAdicional;
    }
}
