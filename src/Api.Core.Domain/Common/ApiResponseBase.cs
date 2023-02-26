namespace Api.Core.Domain.Common;

//[JsonDerivedType(typeof(CrearDocumentoResponse), nameof(CrearDocumentoResponse))]
public abstract class ApiResponseBase
{
    public Guid Id { get; set; }
    public DateTime DateCreated { get; set; } = DateTime.Today;
    public string ErrorMessage { get; set; } = string.Empty;
    public bool IsSuccess { get; set; }
}
