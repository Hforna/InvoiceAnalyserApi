using InvoiceAnalyzer.Domain.Enums;
using InvoiceAnalyzer.Domain.ValueObjects;
using System.Runtime.CompilerServices;

namespace InvoiceAnalyzer.Domain.AggregateRoots;

public class Invoice
{
    public Guid Id { get; set; }
    public Guid SupplierId { get; set; }
    public string InvoiceNumber { get; set; } = null!;
    public DateTime? IssueDate { get; set; }
    public DateTime? DueDate { get; set; }
    public string? Currency { get; set; }
    public decimal? SubtotalAmount { get; set; }
    public decimal? TaxAmount { get; set; }
    public decimal? TotalAmount { get; set; }
    public InvoiceStatus Status { get; set; } = InvoiceStatus.Pending;
    public string? Summary { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }

    public Supplier Supplier { get; set; } = null!;
    public ICollection<InvoiceItem> Items { get; set; } = new List<InvoiceItem>();
    public ICollection<Anomaly> Anomalies { get; set; } = new List<Anomaly>();
    public InvoiceFile? File { get; set; }
}