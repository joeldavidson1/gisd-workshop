using Gisd.Models;
using Gisd.Models.Common;
using Gisd.Models.Invoicing;
using Gisd.Models.Time;

DateOnly today = DateOnly.FromDateTime(DateTime.UtcNow);
DateOnly monthStart = new DateOnly(today.Year, today.Month, 1);
uint daysInMonth = (uint)DateTime.DaysInMonth(today.Year, today.Month);
Period accountingPeriod = new(monthStart, daysInMonth);

ValidatingInvoiceFactory invoiceFactory = new ValidatingInvoiceFactory(
    ServiceDateValidator.RegularInvoiceSubscriptionService(),
    IssueDateValidator.RegularInvoicePosting(accountingPeriod, today));

Company.IdType thisCompanyId = Company.NewId();
Company.IdType otherCompanyId = Company.NewId();
Invoice.IdType invoiceId = new(Guid.NewGuid());
ServiceDate serviceDate = new(DateOnly.FromDateTime(DateTime.UtcNow));
Currency usd = new("USD");

Company thisCompany = new(thisCompanyId, "This Company");
Company otherCompany = new(otherCompanyId, "Other Company");

InvoiceItem item1 = new("Something", "Something, really", new Money(1m, usd), 2);
InvoiceItem item2 = new("Else", "Nothing, really", new Money(2m, usd), 4);
InvoiceItem item3 = new("Something", "Something, really", new Money(1m, usd), 3);
InvoiceItem item4 = new("Something", "Something, really", new Money(2m, usd), 1);

DraftInvoice draftInvoice = invoiceFactory.CreateDraft(
    invoiceId, thisCompany, otherCompany, serviceDate, usd);

draftInvoice.Add(item1);
draftInvoice.Add(item2);
draftInvoice.Add(item3);
draftInvoice.Add(item4);

Console.WriteLine(
    $"Invoicing {draftInvoice.IssuedTo.Name} [{draftInvoice.Currency}]");

foreach (InvoiceItem item in draftInvoice.Items)
{
    Console.WriteLine($" - {item.Name}: {item.Quantity} x {item.UnitPrice}");
}
