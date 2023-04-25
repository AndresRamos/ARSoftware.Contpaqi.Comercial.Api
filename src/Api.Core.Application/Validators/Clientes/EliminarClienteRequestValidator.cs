using Api.Core.Domain.Requests;
using FluentValidation;

namespace Api.Core.Application.Validators.Clientes;

public sealed class EliminarClienteRequestValidator : AbstractValidator<EliminarClienteRequest>
{
    public EliminarClienteRequestValidator()
    {
        RuleFor(c => c.Model.CodigoCliente).NotEmpty();
    }
}
