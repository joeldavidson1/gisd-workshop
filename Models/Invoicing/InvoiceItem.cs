using Gisd.Models.Common;

namespace Gisd.Models.Invoicing;

// RULE #6 - FAVOR IMMUTABLE SHARED OBJECTS OVER RECKLESS ENCAPSULATION
public record InvoiceItem(string Name, string Description, Money UnitPrice, decimal Quantity)
{
    public string Name
    {
        get => field;
        init => field = 
            !string.IsNullOrWhiteSpace(value) ? value
            : throw new ArgumentException("Item name cannot be empty");
    } = Name;

    public string Description
    {
        get => field;
        init => field = 
            !string.IsNullOrWhiteSpace(value) ? value
            : throw new ArgumentException("Item description cannot be empty");
    } = Description;

    public decimal Quantity
    {
        get => field;
        init => field = 
            value > 0 ? value
            : throw new ArgumentException("Quantity must be greater than zero");
    } = Quantity;

    public Money TotalPrice => UnitPrice.Scale(Quantity);
}