namespace Gisd.Models;

public interface IInvoiceFactory
{
    Invoice CreateDraft(
        Company issuedTo, ServiceDate serviceOn, Currency currency);
    Invoice CreateIssued(
        Company issuedTo, ServiceDate serviceOn, Currency currency,
        InvoiceNumber number, IssueDate issuedOn);
}