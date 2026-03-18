using Gisd.Models.Common;
using Gisd.Models.Time;

namespace Gisd.Models.Invoicing;

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