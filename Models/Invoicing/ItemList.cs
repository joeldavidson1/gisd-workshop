using System.Collections;
using Gisd.Models.Common;

namespace Gisd.Models.Invoicing;

public class ItemList : IEnumerable<InvoiceItem>
{
    public Option<Currency> Currency =>
        Items.FirstOrNone().Map(item => item.UnitPrice.Currency);

    private List<InvoiceItem> Items { get; } = new();

    public void Add(InvoiceItem item)
    {
        _ = Currency.Assert(c => c == item.UnitPrice.Currency);

        int existingItemIndex = FindExistingItem(item);
        if (existingItemIndex >= 0)
        {
            Items[existingItemIndex] = Merge(Items[existingItemIndex], item);
            return;
        }

        Items.Add(item);
    }

    private InvoiceItem Merge(InvoiceItem existingItem, InvoiceItem newItem) =>
        existingItem with
        {
            Quantity = existingItem.Quantity + newItem.Quantity
        };

    private int FindExistingItem(InvoiceItem newItem) =>
        Items.FindIndex(i =>
            i.Name == newItem.Name &&
            i.Description == newItem.Description &&
            i.UnitPrice == newItem.UnitPrice);

    public IEnumerator<InvoiceItem> GetEnumerator() =>
        Items.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() =>
        GetEnumerator();
}
