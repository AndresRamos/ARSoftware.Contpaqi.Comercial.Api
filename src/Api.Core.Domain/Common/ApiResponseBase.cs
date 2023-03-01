namespace Api.Core.Domain.Common;

public abstract class ApiResponseBase
{
    public Guid Id { get; set; }
    public DateTime DateCreated { get; set; } = DateTime.Today;
    public string ErrorMessage { get; set; } = string.Empty;
    public bool IsSuccess { get; set; }
    public long ExecutionTime { get; set; }
}
