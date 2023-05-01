using Api.Core.Domain.Models;
using Api.Core.Domain.Requests;
using Api.Sync.Core.Application.ContpaqiComercial.Interfaces;
using ARSoftware.Contpaqi.Comercial.Sdk.DatosAbstractos;
using ARSoftware.Contpaqi.Comercial.Sdk.Extras.Helpers;
using ARSoftware.Contpaqi.Comercial.Sdk.Extras.Interfaces;
using ARSoftware.Contpaqi.Comercial.Sdk.Extras.Models.Enums;
using ARSoftware.Contpaqi.Comercial.Sql.Models.Empresa;
using AutoMapper;
using MediatR;

namespace Api.Sync.Core.Application.Requests.Clientes.CrearCliente;

public sealed class CrearClienteRequestHandler : IRequestHandler<CrearClienteRequest, CrearClienteResponse>
{
    private readonly IClienteProveedorService _clienteProveedorService;
    private readonly IClienteRepository _clienteRepository;
    private readonly IDireccionService _direccionService;
    private readonly IMapper _mapper;

    public CrearClienteRequestHandler(IClienteProveedorService clienteProveedorService, IClienteRepository clienteRepository,
        IMapper mapper, IDireccionService direccionService)
    {
        _clienteProveedorService = clienteProveedorService;
        _clienteRepository = clienteRepository;
        _mapper = mapper;
        _direccionService = direccionService;
    }

    public async Task<CrearClienteResponse> Handle(CrearClienteRequest request, CancellationToken cancellationToken)
    {
        Cliente cliente = request.Model.Cliente;

        int clienteId = _clienteProveedorService.Crear(_mapper.Map<tCteProv>(cliente));

        var datosCliente = new Dictionary<string, string>(cliente.DatosExtra);

        if (cliente.UsoCfdi is not null)
            datosCliente.TryAdd(nameof(admClientes.CUSOCFDI), cliente.UsoCfdi.Clave);

        if (cliente.RegimenFiscal is not null)
            datosCliente.TryAdd(nameof(admClientes.CREGIMFISC), cliente.RegimenFiscal.Clave);

        _clienteProveedorService.Actualizar(clienteId, datosCliente);

        await ActualizarDireccionFiscalAsync(cliente.Codigo, cliente.DireccionFiscal, cancellationToken);

        Cliente clienteActualizado = await _clienteRepository.BuscarPorIdAsync(clienteId, request.Options, cancellationToken) ??
                                     throw new InvalidOperationException();

        return CrearClienteResponse.CreateInstance(clienteActualizado);
    }

    private async Task ActualizarDireccionFiscalAsync(string clienteCodigo, Direccion direccionFiscal, CancellationToken cancellationToken)
    {
        var direccionComercial = _mapper.Map<tDireccion>(direccionFiscal);
        direccionComercial.cCodCteProv = clienteCodigo;
        direccionComercial.cTipoCatalogo = TipoCatalogoDireccionHelper.ConvertToSdkValue(TipoCatalogoDireccion.Clientes);

        if (!await _clienteRepository.ExisteDireccionFiscalDelClienteAsync(clienteCodigo, cancellationToken))
            _direccionService.Crear(direccionComercial);
        else
            _direccionService.Actualizar(direccionComercial);
    }
}
