using Api.Core.Domain.Models;
using Api.Core.Domain.Requests;
using Api.Sync.Core.Application.ContpaqiComercial.Interfaces;
using ARSoftware.Contpaqi.Comercial.Sdk.DatosAbstractos;
using ARSoftware.Contpaqi.Comercial.Sdk.Extras.Interfaces;
using AutoMapper;
using MediatR;

namespace Api.Sync.Core.Application.Requests.Documentos.TimbrarDocumento;

public sealed class TimbrarDocumentoRequestHandler : IRequestHandler<TimbrarDocumentoRequest, TimbrarDocumentoResponse>
{
    private readonly IDocumentoRepository _documentoRepository;
    private readonly IDocumentoService _documentoService;
    private readonly IMapper _mapper;

    public TimbrarDocumentoRequestHandler(IDocumentoService documentoService, IDocumentoRepository documentoRepository, IMapper mapper)
    {
        _documentoService = documentoService;
        _documentoRepository = documentoRepository;
        _mapper = mapper;
    }

    public async Task<TimbrarDocumentoResponse> Handle(TimbrarDocumentoRequest request, CancellationToken cancellationToken)
    {
        string rutaArchivoAdicional = await GetRutaArchivoAdicionalAsync(request, cancellationToken);

        _documentoService.Timbrar(_mapper.Map<tLlaveDoc>(request.Model.LlaveDocumento), request.Model.ContrasenaCertificado,
            rutaArchivoAdicional);

        Documento documento = await _documentoRepository.BuscarPorLlaveAsync(request.Model.LlaveDocumento, request.Options,
            cancellationToken) ?? throw new InvalidOperationException();

        return TimbrarDocumentoResponse.CreateInstance(documento);
    }

    private async Task<string> GetRutaArchivoAdicionalAsync(TimbrarDocumentoRequest request, CancellationToken cancellationToken)
    {
        if (!request.Options.AgregarArchivo)
            return string.Empty;

        string rutaArchivoAdicional = Path.Combine(Path.GetTempPath(), request.Options.NombreArchivo);
        await File.WriteAllTextAsync(rutaArchivoAdicional, request.Options.ContenidoArchivo, cancellationToken);
        return $"Complemento:{rutaArchivoAdicional}";
    }
}
