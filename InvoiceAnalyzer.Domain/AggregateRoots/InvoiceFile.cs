namespace InvoiceAnalyzer.Domain.AggregateRoots;

public class InvoiceFile
{
    public Guid Id { get; set; }
    public Guid InvoiceId { get; set; }
    public string? FileName { get; set; }
    public string? FileType { get; set; }
    public string? FilePath { get; set; }
    public string? ExtractedText { get; set; }
    public string? OcrEngine { get; set; }
    public DateTime ProcessedAt { get; set; } = DateTime.UtcNow;
    public Invoice Invoice { get; set; } = null!;
}