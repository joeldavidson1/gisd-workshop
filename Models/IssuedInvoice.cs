namespace Gisd.Models;

public class IssuedInvoice(
    Company issuedTo, DateOnly serviceOn, Currency currency,bool isAdvance,
    InvoiceNumber number, DateOnly issuedOn)
    : Invoice(issuedTo, serviceOn, currency, isAdvance)
{
    public InvoiceNumber Number { get; } = number;

    public DateOnly IssuedOn { get; } =
        issuedOn > DateOnly.FromDateTime(DateTime.Now) ? throw new ArgumentException("Issued date cannot be in the future.")
        : isAdvance && issuedOn < serviceOn ? throw new ArgumentException("Issued date cannot be before service date for advance invoice.")
        : issuedOn;
}