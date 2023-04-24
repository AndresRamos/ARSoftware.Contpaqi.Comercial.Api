using Api.Core.Domain.Requests;
using Api.Sync.Core.Application.ContpaqiComercial.Interfaces;
using FluentValidation;

namespace Api.Sync.Core.Application.Requests.Clientes.CrearCliente;

public sealed class CrearClienteRequestValidator : AbstractValidator<CrearClienteRequest>
{
    public CrearClienteRequestValidator(IClienteRepository clienteRepository)
    {
        RuleFor(m => m.Model.Cliente.Codigo)
            .MustAsync(async (s, token) => await clienteRepository.ExistePorCodigoAsync(s, token) == false)
            .WithMessage("El cliente {PropertyValue} ya existe.");
    }
}
