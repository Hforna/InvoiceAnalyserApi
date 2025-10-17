using InvoiceAnalyzer.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace InvoiceAnalyzer.Domain.AggregateRoots
{
    public class InvoiceItem
    {

        public Guid Id { get; set; }
        public Guid InvoiceId { get; set; }
        public Invoice Invoice { get; set; }
        public string Description { get; private set; }
        public int Quantity { get; set; }
        public Money UnitPrice { get; set; }
        public Money LineTotal => new(UnitPrice.Amount * Quantity, UnitPrice.Currency);

        public InvoiceItem() { }

        public InvoiceItem(Guid invoiceId, Invoice invoice, string description, int quantity, Money unitPrice)
        {
            InvoiceId = invoiceId;
            Invoice = invoice;
            Description = description;
            Quantity = quantity;
            UnitPrice = unitPrice;
        }
    }
}
