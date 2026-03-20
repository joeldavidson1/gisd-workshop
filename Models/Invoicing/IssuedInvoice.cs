using Gisd.Models.Common;
using Gisd.Models.Time;

namespace Gisd.Models.Invoicing;

public class IssuedInvoice(
    ServiceDateValidator asValidServiceDate, IssueDateValidator asValidIssueDate,
    Invoice.IdType id, Company issuedBy, Company issuedTo, ServiceDate serviceOn, Currency currency,
    InvoiceNumber number, IssueDate issuedOn)
    : Invoice(asValidServiceDate, asValidIssueDate, id, issuedBy, issuedTo, serviceOn, currency)
{
    public InvoiceNumber Number { get; } =
        number.IssuingCompanyId == issuedBy.Id ? number
        : throw new ArgumentException("Invoice number must be issued by the same company as the invoice.");

    public DateOnly IssuedOn { get; } = asValidIssueDate(serviceOn, issuedOn);
}