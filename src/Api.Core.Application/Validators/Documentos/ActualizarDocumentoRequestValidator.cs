using Api.Core.Domain.Requests;
using ARSoftware.Contpaqi.Comercial.Sql.Models.Empresa;
using FluentValidation;

namespace Api.Core.Application.Validators.Documentos;

public sealed class ActualizarDocumentoRequestValidator : AbstractValidator<ActualizarDocumentoRequest>
{
    public ActualizarDocumentoRequestValidator()
    {
        RuleFor(m => m.Model.LlaveDocumento.ConceptoCodigo).NotEmpty();
        RuleFor(m => m.Model.LlaveDocumento.Folio).NotEmpty();
        RuleFor(x => x.Model.DatosDocumento).NotEmpty().ForEach(rule => { rule.SetValidator(new DatosExtraValidator<admDocumentos>()); });
    }
}
