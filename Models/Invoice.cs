namespace Gisd.Models;

public class Invoice(
    InvoiceNumber number, Company issuedTo,
    DateOnly serviceOn, DateOnly issuedOn, Currency currency)
{
    public InvoiceNumber Number { get; set; } = number;
    
    public Company IssuedTo { get; set; } = issuedTo;
    
    public DateOnly ServiceOn
    {
        get => field;
        set => field =
            value <= DateOnly.FromDateTime(DateTime.Now) ? value
            : throw new ArgumentException("Service date cannot be in the future.");
    } = serviceOn;

    public DateOnly IssuedOn
    {
        get => field;
        set => field =
            value <= DateOnly.FromDateTime(DateTime.Now) ? value
            : throw new ArgumentException("Issued date cannot be in the future.");
    } = issuedOn;
    
    public Currency Currency { get; set; } = currency;

    private List<InvoiceItem> ItemsRepresentation { get; } = new();
    public IReadOnlyList<InvoiceItem> Items => ItemsRepresentation.AsReadOnly();
}