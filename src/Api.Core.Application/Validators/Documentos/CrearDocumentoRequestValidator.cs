using Api.Core.Domain.Requests;
using ARSoftware.Contpaqi.Comercial.Sql.Models.Empresa;
using FluentValidation;

namespace Api.Core.Application.Validators.Documentos;

public sealed class CrearDocumentoRequestValidator : AbstractValidator<CrearDocumentoRequest>
{
    public CrearDocumentoRequestValidator()
    {
        RuleFor(m => m.Model.Documento.Concepto.Codigo).NotEmpty();

        RuleFor(x => x.Model.Documento.DatosExtra).ForEach(rule => { rule.SetValidator(new DatosExtraValidator<admDocumentos>()); });
    }
}
