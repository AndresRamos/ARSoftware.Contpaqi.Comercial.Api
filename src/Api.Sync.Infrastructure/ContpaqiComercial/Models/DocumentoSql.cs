// ReSharper disable InconsistentNaming

namespace Api.Sync.Infrastructure.ContpaqiComercial.Models;

public sealed class DocumentoSql
{
    public int CIDDOCUMENTO { get; set; }
    public int CIDCONCEPTODOCUMENTO { get; set; }
    public string CSERIEDOCUMENTO { get; set; } = string.Empty;
    public double CFOLIO { get; set; }
    public DateTime CFECHA { get; set; }
    public int CIDCLIENTEPROVEEDOR { get; set; }
    public int CIDAGENTE { get; set; }
    public double CTIPOCAMBIO { get; set; }
    public string CREFERENCIA { get; set; } = string.Empty;
    public string COBSERVACIONES { get; set; } = string.Empty;
    public int CIDMONEDA { get; set; }
    public double CTOTAL { get; set; }
    public string CMETODOPAG { get; set; } = string.Empty;
    public int CCANTPARCI { get; set; }
}
