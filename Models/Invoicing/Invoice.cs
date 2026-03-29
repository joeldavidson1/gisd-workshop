using Gisd.Models.Common;
using Gisd.Models.Time;

namespace Gisd.Models.Invoicing;

// RULE #9 - IMPLEMENT DEEPLY IMMUTABLE MODELS TO KEEP INVARIANTS AND MAKE SHORTER CODE
public abstract record Invoice(
    ServiceDateValidator AsValidServiceDate, IssueDateValidator AsValidIssueDate,
    Invoice.IdType Id, Company IssuedBy, Company IssuedTo, ServiceDate ServiceOn, Currency Currency, ItemList Items)
{
    public readonly record struct IdType(Guid Value);

    public Invoice.IdType Id { get; } = Id;

    public Company IssuedBy { get; protected init; } = IssuedBy;
    public Company IssuedTo { get; protected init; } = IssuedTo;

    public ServiceDate ServiceOn { get; protected init; } = AsValidServiceDate(ServiceOn);

    public Currency Currency { get; protected init; } = Items.Currency.Assert(c => c == Currency).OrElse(Currency);
    public ItemList Items { get; protected init; } = Items.Currency.Assert(c => c == Currency).Match(_ => Items, () => Items);
}