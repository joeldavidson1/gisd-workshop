using System.Collections;
using Gisd.Models.Common;
using System.Collections.Immutable;

namespace Gisd.Models.Invoicing;

// Invariants:
// - All items must have the same currency
// - (Name, Description, UnitPrice) combination is unique across items, with Quantity summed up
// - Items must appear in the order they were added
public class ItemList : IEnumerable<InvoiceItem>
{
    public Option<Currency> Currency =>
        Items.FirstOrNone().Map(item => item.UnitPrice.Currency);
    
    private Option<Currency> CurrencyWith(InvoiceItem item) =>
        Currency.OrElse(item.UnitPrice.Currency).AsOption().When(c => c == item.UnitPrice.Currency);

    private ImmutableList<InvoiceItem> Items { get; init; } = ImmutableList<InvoiceItem>.Empty;

    private ItemList WithItems(ImmutableList<InvoiceItem> items) =>
        new ItemList() { Items = items };

    public Option<ItemList> Add(InvoiceItem item) =>
        CurrencyWith(item).Map(_ => WithItems(Items.Add(item)));

    public IEnumerator<InvoiceItem> GetEnumerator() =>
        Items.GroupBy(
            i => (i.Name, i.Description, i.UnitPrice),
            (_, items) => items.Aggregate((a, b) => a.AddQuantity(b.Quantity)))
            .GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() =>
        GetEnumerator();
}
