using Api.Core.Domain.Requests;
using Api.Sync.Core.Application.ContpaqiComercial.Interfaces;
using ARSoftware.Contpaqi.Comercial.Sdk.Abstractions.Enums;
using ARSoftware.Contpaqi.Comercial.Sdk.DatosAbstractos;
using ARSoftware.Contpaqi.Comercial.Sdk.Extras.Extensions;
using ARSoftware.Contpaqi.Comercial.Sdk.Extras.Interfaces;
using MediatR;

namespace Api.Sync.Core.Application.Requests.Clientes.CrearCliente;

public sealed class CrearClienteRequestHandler : IRequestHandler<CrearClienteRequest, CrearClienteResponse>
{
    private readonly IClienteProveedorService _clienteProveedorService;
    private readonly IClienteRepository _clienteRepository;
    private readonly IDireccionService _direccionService;

    public CrearClienteRequestHandler(IClienteProveedorService clienteProveedorService, IClienteRepository clienteRepository,
        IDireccionService direccionService)
    {
        _clienteProveedorService = clienteProveedorService;
        _clienteRepository = clienteRepository;
        _direccionService = direccionService;
    }

    public async Task<CrearClienteResponse> Handle(CrearClienteRequest request, CancellationToken cancellationToken)
    {
        int clienteId = _clienteProveedorService.Crear(request.Model.Cliente);

        if (request.Model.Cliente.DireccionFiscal is not null)
            await ActualizarDireccionFiscalAsync(request.Model.Cliente.Codigo, request.Model.Cliente.DireccionFiscal, cancellationToken);

        ClienteProveedor clienteActualizado = await _clienteRepository.BuscarPorIdAsync(clienteId, request.Options, cancellationToken) ??
                                              throw new InvalidOperationException();

        return CrearClienteResponse.CreateInstance(clienteActualizado);
    }

    private async Task ActualizarDireccionFiscalAsync(string clienteCodigo, Direccion direccionFiscal, CancellationToken cancellationToken)
    {
        direccionFiscal.Tipo = TipoDireccion.Fiscal;

        if (!await _clienteRepository.ExisteDireccionFiscalDelClienteAsync(clienteCodigo, cancellationToken))
            _direccionService.Crear(clienteCodigo, direccionFiscal);
        else
        {
            tDireccion sdkDireccion = direccionFiscal.ToSdkDireccion();
            sdkDireccion.cCodCteProv = clienteCodigo;
            _direccionService.Actualizar(sdkDireccion);
        }
    }
}
