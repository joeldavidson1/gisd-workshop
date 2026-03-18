namespace Gisd.Models;

// Issue #1: Instantiation requires knowledge of concrete strategies (delegates)
public abstract class Invoice(
    ServiceDateValidator asValidServiceDate, IssueDateValidator asValidIssueDate,
    Company issuedTo, ServiceDate serviceOn, Currency currency)
{
    public Company IssuedTo { get; } = issuedTo;

    public virtual ServiceDate ServiceOn { get; protected set; } = asValidServiceDate(serviceOn);

    protected ServiceDateValidator AsValidServiceDate { get; } = asValidServiceDate;
    protected IssueDateValidator AsValidIssueDate { get; } = asValidIssueDate;

    public virtual Currency Currency { get; protected set; } = currency;

    protected List<InvoiceItem> ItemsRepresentation { get; } = new();
    public IReadOnlyList<InvoiceItem> Items => ItemsRepresentation.AsReadOnly();
}