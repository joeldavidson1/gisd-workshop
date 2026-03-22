using Gisd.Models.Common;
using Gisd.Models.Time;

namespace Gisd.Models.Invoicing;

public class IssuedInvoice : Invoice
{
    public IssuedInvoice(
        ServiceDateValidator asValidServiceDate, IssueDateValidator asValidIssueDate,
        Invoice.IdType id, Company issuedBy, Company issuedTo,
        ServiceDate serviceOn, Currency currency,
        InvoiceNumber number, IssueDate issuedOn, IEnumerable<InvoiceItem> items)
        : base(asValidServiceDate, asValidIssueDate, id, issuedBy, issuedTo, serviceOn, currency)
    {
        Number =
            number.IssuingCompanyId == issuedBy.Id ? number
            : throw new ArgumentException("Invoice number must be issued by the same company as the invoice.");
        IssuedOn = asValidIssueDate(serviceOn, issuedOn);

        foreach (InvoiceItem item in items) base.Add(item);
    }

    public InvoiceNumber Number { get; }

    public DateOnly IssuedOn { get; }
}