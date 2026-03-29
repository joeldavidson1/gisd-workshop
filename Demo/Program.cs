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

InvoiceNumber invoiceNumber = new(thisCompanyId, today.Year, 19);
IssueDate issueDate = new(today);

IssuedInvoice invoice = invoiceFactory
    .CreateDraft(invoiceId, thisCompany, otherCompany, serviceDate, usd)
    .WithCurrency(new Currency("EUR"))
    .WithCurrency(usd)
    .Add(item1)
    .Add(item2)
    .Add(item3)
    .Add(item4)
    .Issue(invoiceNumber, issueDate);

Console.WriteLine(
    $"Invoicing {invoice.IssuedTo.Name} [{invoice.Currency}]");

foreach (InvoiceItem item in invoice.Items)
{
    Console.WriteLine($" - {item.Name}: {item.Quantity} x {item.UnitPrice}");
}
