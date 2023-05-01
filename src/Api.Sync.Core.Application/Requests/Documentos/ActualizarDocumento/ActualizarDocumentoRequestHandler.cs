using Api.Core.Domain.Models;
using Api.Core.Domain.Requests;
using Api.Sync.Core.Application.ContpaqiComercial.Interfaces;
using ARSoftware.Contpaqi.Comercial.Sdk.DatosAbstractos;
using ARSoftware.Contpaqi.Comercial.Sdk.Extras.Interfaces;
using AutoMapper;
using MediatR;

namespace Api.Sync.Core.Application.Requests.Documentos.ActualizarDocumento;

public sealed class ActualizarDocumentoRequestHandler : IRequestHandler<ActualizarDocumentoRequest, ActualizarDocumentoResponse>
{
    private readonly IDocumentoRepository _documentoRepository;
    private readonly IDocumentoService _documentoService;
    private readonly IMapper _mapper;

    public ActualizarDocumentoRequestHandler(IDocumentoService documentoService, IDocumentoRepository documentoRepository, IMapper mapper)
    {
        _documentoService = documentoService;
        _documentoRepository = documentoRepository;
        _mapper = mapper;
    }

    public async Task<ActualizarDocumentoResponse> Handle(ActualizarDocumentoRequest request, CancellationToken cancellationToken)
    {
        _documentoService.Actualizar(_mapper.Map<tLlaveDoc>(request.Model.LlaveDocumento), request.Model.DatosDocumento);

        Documento documento =
            await _documentoRepository.BuscarPorLlaveAsync(request.Model.LlaveDocumento, request.Options, cancellationToken) ??
            throw new InvalidOperationException();

        return ActualizarDocumentoResponse.CreateInstance(documento);
    }
}
