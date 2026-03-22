using Gisd.Models.Common;
using Gisd.Models.Time;

namespace Gisd.Models.Invoicing;

public abstract class Invoice(
    ServiceDateValidator asValidServiceDate, IssueDateValidator asValidIssueDate,
    Invoice.IdType id, Company issuedBy, Company issuedTo, ServiceDate serviceOn, Currency currency)
{
    public readonly record struct IdType(Guid Value);
    
    public IdType Id { get; } = id;

    public Company IssuedBy { get; } = issuedBy;
    public Company IssuedTo { get; } = issuedTo;

    public virtual ServiceDate ServiceOn { get; protected set; } = asValidServiceDate(serviceOn);

    protected ServiceDateValidator AsValidServiceDate { get; } = asValidServiceDate;
    protected IssueDateValidator AsValidIssueDate { get; } = asValidIssueDate;

    public virtual Currency Currency { get; protected set; } = currency;

    private List<InvoiceItem> ItemsRepresentation { get; } = new();
    public IEnumerable<IReadOnlyInvoiceItem> Items =>
        ItemsRepresentation.OfType<IReadOnlyInvoiceItem>();

    protected virtual void Add(InvoiceItem item)
    {
        if (item.UnitPrice.Currency != Currency)
            throw new ArgumentException("Item currency must match invoice currency");

        if (FindExistingItem(item) is InvoiceItem existingItem)
        {
            existingItem.Quantity += item.Quantity;
            return;
        }

        ItemsRepresentation.Add(item.DeepCopy());
    }

    private InvoiceItem? FindExistingItem(InvoiceItem newItem) =>
        ItemsRepresentation.FirstOrDefault(i =>
            i.Name == newItem.Name &&
            i.Description == newItem.Description &&
            i.UnitPrice == newItem.UnitPrice);
}