using Api.Core.Domain.Models;
using Api.Sync.Core.Application.ContpaqiComercial.Interfaces;
using ARSoftware.Contpaqi.Comercial.Sdk.Extras.Extensions;
using ARSoftware.Contpaqi.Comercial.Sql.Contexts;
using ARSoftware.Contpaqi.Comercial.Sql.Models.Empresa;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace Api.Sync.Infrastructure.ContpaqiComercial;

public sealed class ClienteRepository : IClienteRepository
{
    private readonly ContpaqiComercialEmpresaDbContext _context;
    private readonly IMapper _mapper;

    public ClienteRepository(ContpaqiComercialEmpresaDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<bool> ExistePorCodigoAsync(string codigo, CancellationToken cancellationToken)
    {
        return await _context.admClientes.AnyAsync(c => c.CCODIGOCLIENTE == codigo, cancellationToken);
    }

    public async Task<Cliente?> BuscarPorCodigoAsync(string codigo, CancellationToken cancellationToken)
    {
        admClientes? clienteSql = await _context.admClientes.FirstOrDefaultAsync(c => c.CCODIGOCLIENTE == codigo, cancellationToken);

        if (clienteSql is null)
            return null;

        var cliente = _mapper.Map<Cliente>(clienteSql);

        await CargarObjectosRelacionadosAsync(cliente, clienteSql, cancellationToken);

        return cliente;
    }

    public async Task<Cliente?> BuscarPorIdAsync(int id, CancellationToken cancellationToken)
    {
        admClientes? clienteSql = await _context.admClientes.FirstOrDefaultAsync(c => c.CIDCLIENTEPROVEEDOR == id, cancellationToken);

        if (clienteSql is null)
            return null;

        var cliente = _mapper.Map<Cliente>(clienteSql);

        await CargarObjectosRelacionadosAsync(cliente, clienteSql, cancellationToken);

        return cliente;
    }

    public async Task<IEnumerable<Cliente>> BuscarTodoAsync(CancellationToken cancellationToken)
    {
        var clientesList = new List<Cliente>();

        List<admClientes> clientesSql = await _context.admClientes.OrderBy(c => c.CRAZONSOCIAL).ToListAsync(cancellationToken);

        foreach (admClientes clienteSql in clientesSql)
        {
            var cliente = _mapper.Map<Cliente>(clienteSql);

            await CargarObjectosRelacionadosAsync(cliente, clienteSql, cancellationToken);

            clientesList.Add(cliente);
        }

        return clientesList;
    }

    public async Task<bool> ExisteDireccionFiscalDelClienteAsync(string codigo, CancellationToken cancellationToken)
    {
        // Todo: cambiar  d.CTIPODIRECCION == (int)TipoDireccion.Fiscal y d.CTIPOCATALOGO == (int)TipoCatalogoDireccion.Clientes
        admClientes cliente = await _context.admClientes.FirstAsync(c => c.CCODIGOCLIENTE == codigo, cancellationToken);
        return await _context.admDomicilios.AnyAsync(d =>
                d.CIDCATALOGO == cliente.CIDCLIENTEPROVEEDOR && d.CTIPODIRECCION == 0 && d.CTIPOCATALOGO == 1,
            cancellationToken);
    }

    private async Task CargarObjectosRelacionadosAsync(Cliente cliente, admClientes clienteSql, CancellationToken cancellationToken)
    {
        cliente.DireccionFiscal = await BuscarDireccionFiscalAsync(clienteSql.CIDCLIENTEPROVEEDOR, cancellationToken) ?? new Direccion();

        cliente.DatosExtra = clienteSql.ToDatosDictionary<admClientes>();
    }

    private async Task<Direccion?> BuscarDireccionFiscalAsync(int clienteId, CancellationToken cancellationToken)
    {
        admDomicilios? admDomicilio = await _context.admDomicilios.FirstOrDefaultAsync(d =>
                d.CIDCATALOGO == clienteId && d.CTIPODIRECCION == 0 && d.CTIPOCATALOGO == 1,
            cancellationToken);

        return _mapper.Map<Direccion>(admDomicilio);
    }
}
