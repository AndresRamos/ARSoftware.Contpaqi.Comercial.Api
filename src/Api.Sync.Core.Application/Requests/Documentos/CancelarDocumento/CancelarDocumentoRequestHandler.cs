using Api.Core.Domain.Requests;
using Api.Sync.Core.Application.ContpaqiComercial.Interfaces;
using ARSoftware.Contpaqi.Comercial.Sdk.Abstractions.Enums.CatalogosCfdi;
using ARSoftware.Contpaqi.Comercial.Sdk.DatosAbstractos;
using ARSoftware.Contpaqi.Comercial.Sdk.Extras.Interfaces;
using AutoMapper;
using MediatR;

namespace Api.Sync.Core.Application.Requests.Documentos.CancelarDocumento;

public sealed class CancelarDocumentoRequestHandler : IRequestHandler<CancelarDocumentoRequest, CancelarDocumentoResponse>
{
    private readonly IDocumentoRepository _documentoRepository;
    private readonly IDocumentoService _documentoService;
    private readonly IMapper _mapper;

    public CancelarDocumentoRequestHandler(IDocumentoService documentoService, IDocumentoRepository documentoRepository, IMapper mapper)
    {
        _documentoService = documentoService;
        _documentoRepository = documentoRepository;
        _mapper = mapper;
    }

    public async Task<CancelarDocumentoResponse> Handle(CancelarDocumentoRequest request, CancellationToken cancellationToken)
    {
        var llaveDoc = _mapper.Map<tLlaveDoc>(request.Model.LlaveDocumento);

        if (request.Options.Administrativamente)
            _documentoService.CancelarAdministrativamente(llaveDoc);
        else
        {
            _documentoService.Cancelar(llaveDoc, request.Model.ContrasenaCertificado,
                MotivoCancelacionEnum.FromValue(request.Model.MotivoCancelacion), request.Model.Uuid);
        }

        Documento documento =
            await _documentoRepository.BuscarPorLlaveAsync(request.Model.LlaveDocumento, request.Options, cancellationToken) ??
            throw new InvalidOperationException();

        return CancelarDocumentoResponse.CreateInstance(documento);
    }
}
