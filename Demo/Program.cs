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

DraftInvoice draftInvoice = invoiceFactory.CreateDraft(
    invoiceId, thisCompany, otherCompany, serviceDate, usd);

InvoiceItem item1 = new("Something", "Something, really", new Money(1m, usd), 2);
InvoiceItem item2 = new("Something", "Something, really", new Money(1m, usd), 3);

draftInvoice.Add(item1);
draftInvoice.Add(item2);

InvoiceNumber invoiceNumber = new(thisCompanyId, today.Year, 19);
IssueDate issueDate = new(today);

IssuedInvoice issuedInvoice = draftInvoice.Issue(invoiceNumber, issueDate);

Console.WriteLine($"{issuedInvoice.Number.Year}/{issuedInvoice.Number.SequenceNumber} - {issuedInvoice.IssuedTo.Name}");
foreach (InvoiceItem item in issuedInvoice.Items)
{
    Console.WriteLine($" - {item.Name}: {item.Quantity} x {item.UnitPrice}");
}
