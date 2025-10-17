namespace InvoiceAnalyzer.Domain.AggregateRoots;

public class Anomaly
{
    public Guid Id { get; set; }
    public Guid InvoiceId { get; set; }
    public string Type { get; set; } = null!;
    public string Description { get; set; } = null!;
    public DateTime DetectedAt { get; set; } = DateTime.UtcNow;

    public Invoice Invoice { get; set; } = null!;
}