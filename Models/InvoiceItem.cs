namespace Gisd.Models;

public class InvoiceItem(string name, string description, Money unitPrice, decimal quantity)
{
    public string Name
    {
        get => field;
        set => field = 
            !string.IsNullOrWhiteSpace(value) ? value
            : throw new ArgumentException("Item name cannot be empty");
    } = name;

    public string Description
    {
        get => field;
        set => field = 
            !string.IsNullOrWhiteSpace(value) ? value
            : throw new ArgumentException("Item description cannot be empty");
    } = description;

    public Money UnitPrice { get; set; } = unitPrice;

    public decimal Quantity
    {
        get => field;
        set => field = 
            value > 0 ? value
            : throw new ArgumentException("Quantity must be greater than zero");
    } = quantity;

    public Money TotalPrice() => UnitPrice.Scale(Quantity);
}