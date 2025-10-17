using InvoiceAnalyzer.Domain.Exceptions;

namespace InvoiceAnalyzer.Domain.ValueObjects;

public record Money(decimal Amount, string Currency)
{
    public Money Add(Money money)
    {
        if (!Currency.Equals(money.Currency))
            throw new DomainException("Money currency must be equals");
        return new Money(Amount + money.Amount, Currency);
    }
}