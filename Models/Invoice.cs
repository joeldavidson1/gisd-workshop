namespace Gisd.Models;

public class Invoice(string number, string customerName, DateOnly invoicedOn, string currency)
{
    public string Number { get; set; } = number;
    public string CustomerName { get; set; } = customerName;
    public DateOnly InvoicedOn { get; set; } = invoicedOn;
    public string Currency { get; set; } = currency;
}

// Test comment for my solution