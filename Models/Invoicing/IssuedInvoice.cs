using Gisd.Models.Common;
using Gisd.Models.Time;

namespace Gisd.Models.Invoicing;

public record IssuedInvoice(
    ServiceDateValidator AsValidServiceDate, IssueDateValidator AsValidIssueDate,
    Invoice.IdType Id, Company IssuedBy, Company IssuedTo,
    ServiceDate ServiceOn, Currency Currency, ItemList Items,
    InvoiceNumber Number, IssueDate IssuedOn)
    : Invoice(AsValidServiceDate, AsValidIssueDate, Id, IssuedBy, IssuedTo, ServiceOn, Currency, Items)
{
    public InvoiceNumber Number { get; } =
        Number.IssuingCompanyId == IssuedBy.Id ? Number
        : throw new ArgumentException("Invoice number must be issued by the same company as the invoice.");

    public IssueDate IssuedOn { get; } =
        AsValidIssueDate(ServiceOn, IssuedOn);
}