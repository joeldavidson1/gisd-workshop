using Gisd.Models.Common;
using Gisd.Models.Time;

namespace Gisd.Models.Invoicing;

public class DraftInvoice(
    ServiceDateValidator asValidServiceDate, IssueDateValidator asValidIssueDate,
    Company issuedBy, Company issuedTo, ServiceDate serviceOn, Currency currency)
    : Invoice(asValidServiceDate, asValidIssueDate, issuedBy, issuedTo, serviceOn, currency)
{
    public void WithServiceDate(ServiceDate serviceOn)
    {
        ServiceOn = base.AsValidServiceDate(serviceOn);
    }

    public new void Add(InvoiceItem item) =>
        base.Add(item);

    public IssuedInvoice Issue(InvoiceNumber number, IssueDate issuedOn)
    {
        return new IssuedInvoice(
            base.AsValidServiceDate, base.AsValidIssueDate,
            base.IssuedBy, base.IssuedTo, base.ServiceOn, base.Currency, number, issuedOn);
    }
}
