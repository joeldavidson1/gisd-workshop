using Gisd.Models.Common;
using Gisd.Models.Time;

namespace Gisd.Models.Invoicing;

public interface IInvoiceFactory
{
    Invoice CreateDraft(
        Invoice.IdType id, Company issuedBy,Company issuedTo, ServiceDate serviceOn, Currency currency);
    Invoice CreateIssued(
        Invoice.IdType id, Company issuedBy, Company issuedTo, ServiceDate serviceOn, Currency currency,
        InvoiceNumber number, IssueDate issuedOn);
}