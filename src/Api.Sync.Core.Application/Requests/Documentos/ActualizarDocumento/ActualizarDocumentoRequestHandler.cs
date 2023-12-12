using Api.Core.Domain.Requests;
using Api.Sync.Core.Application.ContpaqiComercial.Interfaces;
using ARSoftware.Contpaqi.Comercial.Sdk.DatosAbstractos;
using ARSoftware.Contpaqi.Comercial.Sdk.Extras.Extensions;
using ARSoftware.Contpaqi.Comercial.Sdk.Extras.Interfaces;
using AutoMapper;
using MediatR;

namespace Api.Sync.Core.Application.Requests.Documentos.ActualizarDocumento;

public sealed class ActualizarDocumentoRequestHandler : IRequestHandler<ActualizarDocumentoRequest, ActualizarDocumentoResponse>
{
    private readonly IDocumentoRepository _documentoRepository;
    private readonly IDocumentoService _documentoService;
    private readonly IMapper _mapper;
    private readonly IContpaqiSdk _sdk;

    public ActualizarDocumentoRequestHandler(IDocumentoService documentoService, IDocumentoRepository documentoRepository, IMapper mapper,
        IContpaqiSdk sdk)
    {
        _documentoService = documentoService;
        _documentoRepository = documentoRepository;
        _mapper = mapper;
        _sdk = sdk;
    }

    public async Task<ActualizarDocumentoResponse> Handle(ActualizarDocumentoRequest request, CancellationToken cancellationToken)
    {
        try
        {
            _documentoService.Actualizar(_mapper.Map<tLlaveDoc>(request.Model.LlaveDocumento), request.Model.DatosDocumento);

            Documento documento =
                await _documentoRepository.BuscarPorLlaveAsync(request.Model.LlaveDocumento, request.Options, cancellationToken) ??
                throw new InvalidOperationException();

            return ActualizarDocumentoResponse.CreateInstance(documento);
        }
        finally
        {
            // TODO: Utilizar el DocumentoService cuando salga la nueva version donde se pueda desbloquear el documento por llave
            _sdk.fBuscarDocumento(request.Model.LlaveDocumento.ConceptoCodigo, request.Model.LlaveDocumento.Serie,
                request.Model.LlaveDocumento.Folio.ToString());
            _sdk.fDesbloqueaDocumento().ToResultadoSdk(_sdk).ThrowIfError();
        }
    }
}
