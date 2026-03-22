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
    public IEnumerable<InvoiceItem> Items => ItemsRepresentation;

    protected virtual void Add(InvoiceItem item)
    {
        if (item.UnitPrice.Currency != Currency)
            throw new ArgumentException("Item currency must match invoice currency");

        int existingItemIndex = FindExistingItem(item);
        if (existingItemIndex >= 0)
        {
            InvoiceItem existingItem = ItemsRepresentation[existingItemIndex];
            ItemsRepresentation[existingItemIndex] = existingItem with
            {
                Quantity = existingItem.Quantity + item.Quantity
            };
            return;
        }

        ItemsRepresentation.Add(item);
    }

    private int FindExistingItem(InvoiceItem newItem) =>
        ItemsRepresentation.FindIndex(i =>
            i.Name == newItem.Name &&
            i.Description == newItem.Description &&
            i.UnitPrice == newItem.UnitPrice);
}