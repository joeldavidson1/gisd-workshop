namespace Gisd.Models;

public class ValidatingInvoiceFactory(
    ServiceDateValidator asValidServiceDate, IssueDateValidator asValidIssueDate)
    : IInvoiceFactory
{
    public Invoice CreateDraft(
        Company issuedTo, ServiceDate serviceOn, Currency currency) =>
        new DraftInvoice(asValidServiceDate, asValidIssueDate, issuedTo, serviceOn, currency);

    public Invoice CreateIssued(
        Company issuedTo, ServiceDate serviceOn, Currency currency,
        InvoiceNumber number, IssueDate issuedOn) =>
        new IssuedInvoice(
            asValidServiceDate, asValidIssueDate,
            issuedTo, serviceOn, currency, number, issuedOn);
}