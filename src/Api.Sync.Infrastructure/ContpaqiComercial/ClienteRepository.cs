using Api.Core.Domain.Common;
using Api.Core.Domain.Models;
using Api.Core.Domain.Requests;
using Api.Sync.Core.Application.ContpaqiComercial.Interfaces;
using Api.Sync.Infrastructure.ContpaqiComercial.Models;
using ARSoftware.Contpaqi.Comercial.Sdk.Extras.Extensions;
using ARSoftware.Contpaqi.Comercial.Sql.Contexts;
using ARSoftware.Contpaqi.Comercial.Sql.Models.Empresa;
using AutoMapper;
using AutoMapper.QueryableExtensions;
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

    public async Task<IEnumerable<Cliente>> BuscarPorRequestModel(BuscarClientesRequestModel requestModel,
                                                                  ILoadRelatedDataOptions loadRelatedDataOptions,
                                                                  CancellationToken cancellationToken)
    {
        var clientesList = new List<Cliente>();

        IQueryable<admClientes> clientesQuery = !string.IsNullOrWhiteSpace(requestModel.SqlQuery)
            ? _context.admClientes.FromSqlRaw($"SELECT * FROM admClientes WHERE {requestModel.SqlQuery}")
            : _context.admClientes.AsQueryable();

        if (requestModel.Id is not null)
            clientesQuery = clientesQuery.Where(a => a.CIDCLIENTEPROVEEDOR == requestModel.Id);

        if (!string.IsNullOrWhiteSpace(requestModel.Codigo))
            clientesQuery = clientesQuery.Where(a => a.CCODIGOCLIENTE == requestModel.Codigo);

        List<ClienteSql> clientesSql = await clientesQuery
            .ProjectTo<ClienteSql>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);

        foreach (ClienteSql? clienteSql in clientesSql)
        {
            var cliente = _mapper.Map<Cliente>(clienteSql);

            await CargarDatosRelacionadosAsync(cliente, clienteSql, loadRelatedDataOptions, cancellationToken);

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

    public async Task<Cliente?> BuscarPorCodigoAsync(string codigo,
                                                     ILoadRelatedDataOptions loadRelatedDataOptions,
                                                     CancellationToken cancellationToken)
    {
        ClienteSql? clienteSql = await _context.admClientes.Where(c => c.CCODIGOCLIENTE == codigo)
            .ProjectTo<ClienteSql>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(cancellationToken);

        if (clienteSql is null)
            return null;

        var cliente = _mapper.Map<Cliente>(clienteSql);

        await CargarDatosRelacionadosAsync(cliente, clienteSql, loadRelatedDataOptions, cancellationToken);

        return cliente;
    }

    public async Task<Cliente?> BuscarPorIdAsync(int id,
                                                 ILoadRelatedDataOptions loadRelatedDataOptions,
                                                 CancellationToken cancellationToken)
    {
        ClienteSql? clienteSql = await _context.admClientes.Where(c => c.CIDCLIENTEPROVEEDOR == id)
            .ProjectTo<ClienteSql>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(cancellationToken);

        if (clienteSql is null)
            return null;

        var cliente = _mapper.Map<Cliente>(clienteSql);

        await CargarDatosRelacionadosAsync(cliente, clienteSql, loadRelatedDataOptions, cancellationToken);

        return cliente;
    }

    public async Task<IEnumerable<Cliente>> BuscarTodoAsync(ILoadRelatedDataOptions loadRelatedDataOptions,
                                                            CancellationToken cancellationToken)
    {
        var clientesList = new List<Cliente>();

        List<ClienteSql> clientesSql = await _context.admClientes.OrderBy(c => c.CRAZONSOCIAL)
            .ProjectTo<ClienteSql>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);

        foreach (ClienteSql? clienteSql in clientesSql)
        {
            var cliente = _mapper.Map<Cliente>(clienteSql);

            await CargarDatosRelacionadosAsync(cliente, clienteSql, loadRelatedDataOptions, cancellationToken);

            clientesList.Add(cliente);
        }

        return clientesList;
    }

    private async Task CargarDatosRelacionadosAsync(Cliente cliente,
                                                    ClienteSql clienteSql,
                                                    ILoadRelatedDataOptions loadRelatedDataOptions,
                                                    CancellationToken cancellationToken)
    {
        cliente.DireccionFiscal = await BuscarDireccionFiscalAsync(clienteSql.CIDCLIENTEPROVEEDOR, cancellationToken) ?? new Direccion();

        if (loadRelatedDataOptions.CargarDatosExtra)
            cliente.DatosExtra =
                (await _context.admClientes.FirstAsync(c => c.CIDCLIENTEPROVEEDOR == clienteSql.CIDCLIENTEPROVEEDOR, cancellationToken))
                .ToDatosDictionary<admClientes>();
    }

    private async Task<Direccion?> BuscarDireccionFiscalAsync(int clienteId, CancellationToken cancellationToken)
    {
        admDomicilios? admDomicilio = await _context.admDomicilios.FirstOrDefaultAsync(d =>
                d.CIDCATALOGO == clienteId && d.CTIPODIRECCION == 0 && d.CTIPOCATALOGO == 1,
            cancellationToken);

        return _mapper.Map<Direccion>(admDomicilio);
    }
}
