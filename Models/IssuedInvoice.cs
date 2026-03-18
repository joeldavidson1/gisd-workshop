namespace Gisd.Models;

public class IssuedInvoice(
    ServiceDateValidator asValidServiceDate, IssueDateValidator asValidIssueDate,
    Company issuedTo, ServiceDate serviceOn, Currency currency,
    InvoiceNumber number, IssueDate issuedOn)
    : Invoice(asValidServiceDate, asValidIssueDate, issuedTo, serviceOn, currency)
{
    public InvoiceNumber Number { get; } = number;

    public DateOnly IssuedOn { get; } = asValidIssueDate(serviceOn, issuedOn);
}