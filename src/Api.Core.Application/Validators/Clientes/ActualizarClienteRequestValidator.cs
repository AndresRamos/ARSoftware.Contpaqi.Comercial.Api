using Api.Core.Domain.Requests;
using ARSoftware.Contpaqi.Comercial.Sql.Models.Empresa;
using FluentValidation;

namespace Api.Core.Application.Validators.Clientes;

public sealed class ActualizarClienteRequestValidator : AbstractValidator<ActualizarClienteRequest>
{
    public ActualizarClienteRequestValidator()
    {
        RuleFor(c => c.Model.CodigoCliente).NotEmpty();

        RuleFor(x => x.Model.DatosCliente).NotEmpty().ForEach(rule => rule.SetValidator(new DatosExtraValidator<admClientes>()));
    }
}
