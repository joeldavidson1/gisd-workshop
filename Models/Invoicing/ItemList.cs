using System.Collections;
using Gisd.Models.Common;
using System.Diagnostics.CodeAnalysis;

namespace Gisd.Models.Invoicing;

// RULE #7 - MAKE TYPES FOR THE SAKE OF IMPLEMENTING PRECONDITIONS, POSTCONDITIONS, AND INVARIANTS

// Class invariants:
// #1 - All items must have the same currency
// #2 - Only one item with same name, description and unit price exists
// #3 - Currency is non-null when HasItems is true
public class ItemList : IEnumerable<InvoiceItem>
{
    public Currency? Currency =>
        Items.FirstOrDefault()?.UnitPrice.Currency;
    
    [MemberNotNullWhen(true, nameof(Currency))]
    public bool HasItems =>
        Items.Any();

    private List<InvoiceItem> Items { get; } = new();

    // Method preconditions:
    // #1 - New item's currency must match the currency of existing items
    // Method postconditions:
    // #1 - Quantity of items with (name, description, price) increased by item's quantity
    // #2 - Currency is non-null
    public void Add(InvoiceItem item)
    {
        if (Currency is not null && item.UnitPrice.Currency != Currency)
            throw new ArgumentException("All items must have the same currency");

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
