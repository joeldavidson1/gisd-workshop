using Gisd.Models.Common;
using Gisd.Models.Time;

namespace Gisd.Models.Invoicing;

public class ValidatingInvoiceFactory(
    ServiceDateValidator asValidServiceDate, IssueDateValidator asValidIssueDate)
    : IInvoiceFactory
{
    Invoice IInvoiceFactory.CreateDraft(
        Invoice.IdType id, Company issuedBy, Company issuedTo, ServiceDate serviceOn, Currency currency) =>
        this.CreateDraft(id, issuedBy, issuedTo, serviceOn, currency);

    public DraftInvoice CreateDraft(
        Invoice.IdType id, Company issuedBy, Company issuedTo, ServiceDate serviceOn, Currency currency) =>
        new DraftInvoice(asValidServiceDate, asValidIssueDate, id, issuedBy, issuedTo, serviceOn, currency);

    Invoice IInvoiceFactory.CreateIssued(
        Invoice.IdType id, Company issuedBy, Company issuedTo, ServiceDate serviceOn, Currency currency,
        InvoiceNumber number, IssueDate issuedOn) =>
        this.CreateIssued(id, issuedBy, issuedTo, serviceOn, currency, number, issuedOn);

    public IssuedInvoice CreateIssued(
        Invoice.IdType id, Company issuedBy, Company issuedTo, ServiceDate serviceOn, Currency currency,
        InvoiceNumber number, IssueDate issuedOn) =>
        new IssuedInvoice(
            asValidServiceDate, asValidIssueDate,
            id, issuedBy, issuedTo, serviceOn, currency, number, issuedOn);
}