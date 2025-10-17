using InvoiceAnalyzer.Domain.AggregateRoots;
using InvoiceAnalyzer.Domain.Entities;
using InvoiceAnalyzer.Domain.Enums;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace InvoiceAnalyzer.Infrastructure.Persistence;

public class DataContext : IdentityDbContext<User, Role, Guid>
{
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {

    }

    public DbSet<Anomaly> Anomalies { get; set; }
    public DbSet<Invoice> Invoices { get; set; }
    public DbSet<InvoiceItem> InvoicesItem { get; set; }
    public DbSet<InvoiceFile> InvoicesFile { get; set; }
    public DbSet<Supplier> Suppliers { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        ConfigureSupplier(modelBuilder);
        ConfigureInvoice(modelBuilder);
        ConfigureInvoiceItem(modelBuilder);
        ConfigureAnomaly(modelBuilder);
        ConfigureInvoiceFile(modelBuilder);
    }

    private void ConfigureSupplier(ModelBuilder modelBuilder)
    {
        var builder = modelBuilder.Entity<Supplier>();
        builder.ToTable("Suppliers");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Name).HasMaxLength(255).IsRequired();
        builder.Property(x => x.TaxId).HasMaxLength(50);
        builder.Property(x => x.Email).HasMaxLength(255);
        builder.Property(x => x.Phone).HasMaxLength(50);
        builder.Property(x => x.Address).HasMaxLength(255);
        builder.Property(x => x.Country).HasMaxLength(100);

        builder.HasMany(x => x.Invoices)
               .WithOne(x => x.Supplier)
               .HasForeignKey(x => x.SupplierId)
               .OnDelete(DeleteBehavior.Restrict);
    }

    private void ConfigureInvoice(ModelBuilder modelBuilder)
    {
        var builder = modelBuilder.Entity<Invoice>();
        builder.ToTable("Invoices");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.InvoiceNumber).HasMaxLength(100).IsRequired();
        builder.Property(x => x.Currency).HasMaxLength(10);
        builder.Property(x => x.Status)
               .HasConversion<string>()
               .HasMaxLength(50)
               .HasDefaultValue(InvoiceStatus.Pending);

        builder.Property(x => x.SubtotalAmount).HasColumnType("decimal(18,2)");
        builder.Property(x => x.TaxAmount).HasColumnType("decimal(18,2)");
        builder.Property(x => x.TotalAmount).HasColumnType("decimal(18,2)");
        builder.Property(x => x.Summary);
        builder.Property(x => x.CreatedAt).HasDefaultValueSql("GETUTCDATE()");

        builder.HasMany(x => x.Items)
               .WithOne(x => x.Invoice)
               .HasForeignKey(x => x.InvoiceId)
               .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(x => x.Anomalies)
               .WithOne(x => x.Invoice)
               .HasForeignKey(x => x.InvoiceId)
               .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(x => x.File)
               .WithOne(x => x.Invoice)
               .HasForeignKey<InvoiceFile>(x => x.InvoiceId)
               .OnDelete(DeleteBehavior.Cascade);
    }

    private void ConfigureInvoiceItem(ModelBuilder modelBuilder)
    {
        var builder = modelBuilder.Entity<InvoiceItem>();
        builder.ToTable("InvoiceItems");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Description).HasMaxLength(255);
        builder.Property(x => x.Quantity).HasColumnType("decimal(10,2)");
        builder.Property(x => x.UnitPrice.Amount).HasColumnType("decimal(18,2)");
        builder.Property(x => x.UnitPrice.Currency).HasMaxLength(10);
        builder.Property(x => x.LineTotal).HasColumnType("decimal(18,2)");

        builder.HasOne(x => x.Invoice)
               .WithMany(x => x.Items)
               .HasForeignKey(x => x.InvoiceId)
               .OnDelete(DeleteBehavior.Cascade);
    }

    private void ConfigureAnomaly(ModelBuilder modelBuilder)
    {
        var builder = modelBuilder.Entity<Anomaly>();
        builder.ToTable("Anomalies");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Type).HasMaxLength(100).IsRequired();
        builder.Property(x => x.Description).IsRequired();
        builder.Property(x => x.DetectedAt).HasDefaultValueSql("GETUTCDATE()");

        builder.HasOne(x => x.Invoice)
               .WithMany(x => x.Anomalies)
               .HasForeignKey(x => x.InvoiceId)
               .OnDelete(DeleteBehavior.Cascade);
    }

    private void ConfigureInvoiceFile(ModelBuilder modelBuilder)
    {
        var builder = modelBuilder.Entity<InvoiceFile>();
        builder.ToTable("InvoiceFiles");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.FileName).HasMaxLength(255);
        builder.Property(x => x.FileType).HasMaxLength(50);
        builder.Property(x => x.OcrEngine).HasMaxLength(100);
        builder.Property(x => x.ProcessedAt).HasDefaultValueSql("GETUTCDATE()");

        builder.HasOne(x => x.Invoice)
               .WithOne(x => x.File)
               .HasForeignKey<InvoiceFile>(x => x.InvoiceId)
               .OnDelete(DeleteBehavior.Cascade);
    }
}