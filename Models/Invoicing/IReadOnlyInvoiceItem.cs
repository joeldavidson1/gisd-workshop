using Gisd.Models.Common;

namespace Gisd.Models.Invoicing;

public interface IReadOnlyInvoiceItem
{
    string Name { get; }
    string Description { get; }
    Money UnitPrice { get; }
    decimal Quantity { get; }
    Money TotalPrice { get; }
}
