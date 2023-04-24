using Api.Core.Domain.Requests;
using ARSoftware.Contpaqi.Comercial.Sql.Models.Empresa;
using FluentValidation;

namespace Api.Core.Application.Validators.Agentes;

public sealed class CrearAgenteRequestValidator : AbstractValidator<CrearAgenteRequest>
{
    public CrearAgenteRequestValidator()
    {
        RuleFor(c => c.Model.Agente.Codigo).NotEmpty();

        RuleFor(c => c.Model.Agente.Nombre).NotEmpty();

        RuleFor(c => c.Model.Agente.Tipo).NotEmpty();

        RuleFor(x => x.Model.Agente.DatosExtra).ForEach(rule => { rule.SetValidator(new DatosExtraValidator<admAgentes>()); });
    }
}
