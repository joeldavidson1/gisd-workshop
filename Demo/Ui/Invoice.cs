using Gisd.Models.Common;
using Gisd.Models.Time;
using Gisd.Models.Invoicing;

namespace Gisd.Demo.Ui;

public record Invoice(
    Guid Id, int? InvoiceYear, int? InvoiceSequence,
    Company IssuedBy, Company IssuedTo,
    DateOnly ServiceOn, DateOnly? IssuedOn,
    InvoiceItem[] Items)
{
    public bool IsDraft => !InvoiceYear.HasValue || !InvoiceSequence.HasValue || !IssuedOn.HasValue;

    public static Invoice FromModel(Gisd.Models.Invoicing.Invoice invoice) =>
        new Invoice(
            invoice.Id.Value,
            invoice is IssuedInvoice { Number: { Year: var year } } ? year : null,
            invoice is IssuedInvoice { Number: { Sequence: var sequence } } ? sequence : null,
            new Company(invoice.IssuedBy.Id.Value, invoice.IssuedBy.Name),
            new Company(invoice.IssuedTo.Id.Value, invoice.IssuedTo.Name),
            invoice.ServiceOn.Value,
            invoice is IssuedInvoice { IssuedOn: { } issuedOn } ? issuedOn : null,
            invoice.Items.Select(InvoiceItem.FromModel).ToArray());
    
    public Invoice ToModel() =>
        throw new NotImplementedException("Might never be implemented.");
}

public record Company(
    Guid Id, string Name);

public record InvoiceItem(
    string Description, Money Amount, decimal Quantity)
{
    public static InvoiceItem FromModel(Gisd.Models.Invoicing.InvoiceItem model) =>
        new InvoiceItem(model.Description, Money.FromModel(model.UnitPrice), model.Quantity);
}

public record Money(decimal Amount, string Currency)
{
    public static Money FromModel(Gisd.Models.Common.Money model) =>
        new Money(model.Amount, model.Currency.Symbol);
}