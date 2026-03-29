using Gisd.Models.Common;

namespace Gisd.Models.Invoicing.Documenting;

public abstract record InvoiceLine(Money Total);
public record ServiceLine(string Description, Money UnitCost, decimal Quantity)
    : InvoiceLine(UnitCost.Scale(Quantity));
public record TimeServiceLine(string Description, Money HourlyRate, TimeSpan Duration)
    : InvoiceLine(HourlyRate.Scale((decimal)Duration.TotalHours));
public record MaterialLine(string Description, Money UnitCost, decimal Quantity)
    : InvoiceLine(UnitCost.Scale(Quantity));
public record ExpenseLine(string Description, Money Total)
    : InvoiceLine(Total);
public record TaxLine(string Description, decimal TaxRate, Money TaxBase)
    : InvoiceLine(TaxBase.Scale(TaxRate));
public record Subtotal(string Description, Money Total)
    : InvoiceLine(Total);
public record GrandTotal(Money Total)
    : InvoiceLine(Total);