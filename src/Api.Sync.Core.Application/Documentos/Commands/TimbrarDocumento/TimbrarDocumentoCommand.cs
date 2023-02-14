using Api.Sync.Core.Application.ContpaqiComercial.Interfaces;
using ARSoftware.Contpaqi.Comercial.Sdk.DatosAbstractos;
using ARSoftware.Contpaqi.Comercial.Sdk.Extras.Interfaces;
using MediatR;

namespace Api.Sync.Core.Application.Documentos.Commands.TimbrarDocumento;

public sealed record TimbrarDocumentoCommand(int DocumentoId, string CertificadoContrasena) : IRequest;

public sealed class TimbrarDocumentoCommandHandler : IRequestHandler<TimbrarDocumentoCommand>
{
    private readonly IDocumentoRepository _documentoRepository;
    private readonly IDocumentoService _documentoService;

    public TimbrarDocumentoCommandHandler(IDocumentoService documentoService, IDocumentoRepository documentoRepository)
    {
        _documentoService = documentoService;
        _documentoRepository = documentoRepository;
    }

    public async Task<Unit> Handle(TimbrarDocumentoCommand request, CancellationToken cancellationToken)
    {
        tLlaveDoc documento = await _documentoRepository.BuscarLlavePorIdAsync(request.DocumentoId, cancellationToken);
        _documentoService.Timbrar(documento, request.CertificadoContrasena);
        return Unit.Value;
    }
}
