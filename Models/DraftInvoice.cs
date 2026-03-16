namespace Gisd.Models;

public class DraftInvoice(Company issuedTo, DateOnly serviceOn, Currency currency, bool isAdvance)
    : Invoice(issuedTo, serviceOn, currency, isAdvance)
{
    public void WithServiceDate(DateOnly serviceOn)
    {
        ServiceOn = AsValidServiceDate(serviceOn, base.IsAdvance);
    }

    public void Add(InvoiceItem item)
    {
        base.ItemsRepresentation.Add(item);
    }

    public IssuedInvoice Issue(InvoiceNumber number, DateOnly issuedOn)
    {
        return new IssuedInvoice(base.IssuedTo, base.ServiceOn, base.Currency, base.IsAdvance, number, issuedOn);
    }
}
