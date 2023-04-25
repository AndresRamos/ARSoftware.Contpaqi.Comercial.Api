using Api.Core.Domain.Requests;
using ARSoftware.Contpaqi.Comercial.Sql.Models.Empresa;
using FluentValidation;

namespace Api.Core.Application.Validators.Clientes;

public sealed class CrearClienteRequestValidator : AbstractValidator<CrearClienteRequest>
{
    public CrearClienteRequestValidator()
    {
        RuleFor(c => c.Model.Cliente.Tipo).NotEmpty();
        RuleFor(c => c.Model.Cliente.Codigo).NotEmpty();
        RuleFor(c => c.Model.Cliente.RazonSocial).NotEmpty();
        RuleFor(c => c.Model.Cliente.Rfc).NotNull();
        RuleFor(x => x.Model.Cliente.DatosExtra).ForEach(rule => { rule.SetValidator(new DatosExtraValidator<admClientes>()); });
    }
}
