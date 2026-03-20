using Gisd.Models.Common;
using Gisd.Models.Time;

namespace Gisd.Models.Invoicing;

public class ValidatingInvoiceFactory(
    ServiceDateValidator asValidServiceDate, IssueDateValidator asValidIssueDate)
    : IInvoiceFactory
{
    public Invoice CreateDraft(
        Invoice.IdType id, Company issuedBy, Company issuedTo, ServiceDate serviceOn, Currency currency) =>
        new DraftInvoice(asValidServiceDate, asValidIssueDate, id, issuedBy, issuedTo, serviceOn, currency);

    public Invoice CreateIssued(
        Invoice.IdType id, Company issuedBy, Company issuedTo, ServiceDate serviceOn, Currency currency,
        InvoiceNumber number, IssueDate issuedOn) =>
        new IssuedInvoice(
            asValidServiceDate, asValidIssueDate,
            id, issuedBy, issuedTo, serviceOn, currency, number, issuedOn);
}