using Api.Core.Domain.Requests;
using Api.Sync.Core.Application.Clientes.Commands.CreateCliente;
using Api.Sync.Core.Application.ContpaqiComercial.Interfaces;
using MediatR;

namespace Api.Sync.Core.Application.ContpaqiComercialApi.Commands.ProcessCreateClienteRequest;

public sealed record ProcessCreateClienteRequestCommand(CreateClienteRequest ApiRequest) : IRequest<CreateClienteResponse>;

public sealed class ProcessCreateClienteRequestCommandHandler : IRequestHandler<ProcessCreateClienteRequestCommand, CreateClienteResponse>
{
    private readonly IClienteRepository _clienteRepository;
    private readonly IMediator _mediator;

    public ProcessCreateClienteRequestCommandHandler(IMediator mediator, IClienteRepository clienteRepository)
    {
        _mediator = mediator;
        _clienteRepository = clienteRepository;
    }

    public async Task<CreateClienteResponse> Handle(ProcessCreateClienteRequestCommand request, CancellationToken cancellationToken)
    {
        CreateClienteRequest apiRequest = request.ApiRequest;

        await _mediator.Send(new CreateClienteCommand(apiRequest.Model, apiRequest.Options), cancellationToken);

        return CreateClienteResponse.CreateSuccessfull(apiRequest,
            await _clienteRepository.BuscarPorCodigoAsync(apiRequest.Model.Codigo, cancellationToken));
    }
}
