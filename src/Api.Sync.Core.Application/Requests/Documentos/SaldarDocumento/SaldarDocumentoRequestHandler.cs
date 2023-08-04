using Api.Core.Domain.Requests;
using Api.Sync.Core.Application.ContpaqiComercial.Interfaces;
using ARSoftware.Contpaqi.Comercial.Sdk.DatosAbstractos;
using ARSoftware.Contpaqi.Comercial.Sdk.Extras.Interfaces;
using AutoMapper;
using MediatR;

namespace Api.Sync.Core.Application.Requests.Documentos.SaldarDocumento;

public sealed class SaldarDocumentoRequestHandler : IRequestHandler<SaldarDocumentoRequest, SaldarDocumentoResponse>
{
    private readonly IDocumentoRepository _documentoRepository;
    private readonly IDocumentoService _documentoService;
    private readonly IMapper _mapper;

    public SaldarDocumentoRequestHandler(IDocumentoService documentoService, IDocumentoRepository documentoRepository, IMapper mapper)
    {
        _documentoService = documentoService;
        _documentoRepository = documentoRepository;
        _mapper = mapper;
    }

    public async Task<SaldarDocumentoResponse> Handle(SaldarDocumentoRequest request, CancellationToken cancellationToken)
    {
        _documentoService.SaldarDocumento(_mapper.Map<tLlaveDoc>(request.Model.DocumentoAPagar),
            _mapper.Map<tLlaveDoc>(request.Model.DocumentoPago), request.Model.Fecha, (double)request.Model.Importe);

        Documento documentoPagar =
            await _documentoRepository.BuscarPorLlaveAsync(request.Model.DocumentoAPagar, request.Options, cancellationToken) ??
            throw new InvalidOperationException();

        Documento documentoPago =
            await _documentoRepository.BuscarPorLlaveAsync(request.Model.DocumentoPago, request.Options, cancellationToken) ??
            throw new InvalidOperationException();

        return SaldarDocumentoResponse.CreateInstance(documentoPago, documentoPagar);
    }
}
