namespace Gisd.Models;

public abstract class Invoice(Company issuedTo, DateOnly serviceOn, Currency currency, bool isAdvance)
{
    // RULE #2: PREFER BOOL AS AN ANSWER, NOT AS A STATE

    public Company IssuedTo { get; } = issuedTo;

    public bool IsAdvance { get; } = isAdvance;
    
    public virtual DateOnly ServiceOn { get; protected set; } = AsValidServiceDate(serviceOn, isAdvance);

    protected static DateOnly AsValidServiceDate(DateOnly serviceOn, bool isAdvance) =>
        serviceOn <= DateOnly.FromDateTime(DateTime.Now) ? serviceOn
        : isAdvance ? serviceOn
        : throw new ArgumentException("Service date cannot be in the future.");

    public virtual Currency Currency { get; protected set; } = currency;

    protected List<InvoiceItem> ItemsRepresentation { get; } = new();
    public IReadOnlyList<InvoiceItem> Items => ItemsRepresentation.AsReadOnly();
}