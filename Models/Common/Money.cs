namespace Gisd.Models.Common;

public record Money(decimal amount, Currency currency)
{
    public decimal Amount
    {
        get => field;
        set => field =
            value >= 0 ? value
            : throw new ArgumentException("Amount cannot be negative");
    } = amount;

    public Currency Currency { get; set; } = currency;

    public Money Add(Money other)
    {
        if (Currency != other.Currency)
            throw new InvalidOperationException("Cannot add amounts with different currencies");

        return new Money(Amount + other.Amount, Currency);
    }

    public Money Scale(decimal factor)
    {
        if (factor < 0)
            throw new ArgumentException("Scale factor cannot be negative");

        return new Money(Amount * factor, Currency);
    }

    public override string ToString() => $"{Amount:N2} {Currency}";
}