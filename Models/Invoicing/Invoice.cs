using Gisd.Models.Common;
using Gisd.Models.Time;

namespace Gisd.Models.Invoicing;

// Class invariants:
// 1. Invoice currency the same through all items and invoice itself
// 2. There can be only one item with same name, description and unit price
public abstract class Invoice(
    ServiceDateValidator asValidServiceDate, IssueDateValidator asValidIssueDate,
    Company issuedTo, ServiceDate serviceOn, Currency currency)
{
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