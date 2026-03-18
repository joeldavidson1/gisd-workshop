namespace Gisd.Models;

public class DraftInvoice(
    ServiceDateValidator asValidServiceDate, IssueDateValidator asValidIssueDate,
    Company issuedTo, ServiceDate serviceOn, Currency currency)
    : Invoice(asValidServiceDate, asValidIssueDate,issuedTo, serviceOn, currency)
{
    public void WithServiceDate(ServiceDate serviceOn)
    {
        ServiceOn = base.AsValidServiceDate(serviceOn);
    }

    public void Add(InvoiceItem item)
    {
        base.ItemsRepresentation.Add(item);
    }

    public IssuedInvoice Issue(InvoiceNumber number, IssueDate issuedOn)
    {
        return new IssuedInvoice(
            base.AsValidServiceDate, base.AsValidIssueDate,
            base.IssuedTo, base.ServiceOn, base.Currency, number, issuedOn);
    }
}
