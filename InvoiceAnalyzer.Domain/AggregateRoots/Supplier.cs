namespace InvoiceAnalyzer.Domain.AggregateRoots;

public class Supplier
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public string? TaxId { get; set; }
    public string? Email { get; set; }
    public string? Phone { get; set; }
    public string? Address { get; set; }
    public string? Country { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public ICollection<Invoice> Invoices { get; set; } = new List<Invoice>();
}