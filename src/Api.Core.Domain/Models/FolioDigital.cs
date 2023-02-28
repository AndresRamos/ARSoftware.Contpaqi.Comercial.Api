namespace Api.Core.Domain.Models;

public sealed class FolioDigital
{
    public string Uuid { get; set; } = string.Empty;
    public Dictionary<string, string> DatosExtra { get; set; } = new();
}
