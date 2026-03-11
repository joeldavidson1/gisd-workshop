namespace Gisd.Models;

public class Invoice(InvoiceNumber number, Company issuedTo, DateOnly invoicedOn, Currency currency)
{
    // RULE #1: IF YOU HAVE AN OBJECT, IT'S FINE
   
    public InvoiceNumber Number { get; set; } = number;
    
    public Company IssuedTo { get; set; } = issuedTo;
    
    public DateOnly InvoicedOn
    {
        get => field;
        set => field =
            value <= DateOnly.FromDateTime(DateTime.Now) ? value
           : throw new ArgumentException("InvoicedOn cannot be a future date.");
    } = invoicedOn;
    
    public Currency Currency { get; set; } = currency;
}