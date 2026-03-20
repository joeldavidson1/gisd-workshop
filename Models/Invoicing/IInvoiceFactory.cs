using Gisd.Models.Common;
using Gisd.Models.Time;

namespace Gisd.Models.Invoicing;

public interface IInvoiceFactory
{
    Invoice CreateDraft(
        Company issuedBy,Company issuedTo, ServiceDate serviceOn, Currency currency);
    Invoice CreateIssued(
        Company issuedBy, Company issuedTo, ServiceDate serviceOn, Currency currency,
        InvoiceNumber number, IssueDate issuedOn);
}