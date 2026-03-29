using Gisd.Models.Common;
using Gisd.Models.Time;

namespace Gisd.Models.Invoicing;

public record DraftInvoice(
    ServiceDateValidator AsValidServiceDate, IssueDateValidator AsValidIssueDate,
    Invoice.IdType Id, Company IssuedBy, Company IssuedTo, ServiceDate ServiceOn, Currency Currency, ItemList Items)
    : Invoice(AsValidServiceDate, AsValidIssueDate, Id, IssuedBy, IssuedTo, ServiceOn, Currency, Items)
{
    public DraftInvoice WithServiceDate(ServiceDate serviceOn) =>
        this with { ServiceOn = AsValidServiceDate(serviceOn) };

    public DraftInvoice WithCurrency(Currency currency) =>
        this with { Currency = currency };

    public DraftInvoice Add(InvoiceItem item) =>
        this with { Items = Items.Add(item).OrElse(() => throw new ArgumentException("Failed to add item")) };
    
    public DraftInvoice IssuedByCompany(Company issuedBy) =>
        this with { IssuedBy = issuedBy };
    
    public DraftInvoice IssuedToCompany(Company issuedTo) =>
        this with { IssuedTo = issuedTo };

    public IssuedInvoice Issue(InvoiceNumber number, IssueDate issuedOn) =>
        new IssuedInvoice(
            AsValidServiceDate, AsValidIssueDate, Id, IssuedBy,
            IssuedTo, ServiceOn, Currency, Items, number, issuedOn);
}
