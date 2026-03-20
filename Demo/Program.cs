using Gisd.Models.Common;
using Gisd.Models.Invoicing;
using Gisd.Models.Time;
using Gisd.Models;

Money oneDollar = new Money(1.00m, new Currency("USD"));
Money oneEuro = new Money(1.00m, new Currency("EUR"));

HashSet<Money> wallet = [ oneDollar ];

bool contains = wallet.Contains(new Money(1.00m, new Currency("USD"))); // true
Console.WriteLine($"Wallet contains one dollar: {contains}");

Console.WriteLine($"{oneDollar} == {oneEuro}: {oneDollar == oneEuro}"); // false

// oneDollar.Currency = new Currency("EUR");

Console.WriteLine($"{oneDollar} == {oneEuro}: {oneDollar == oneEuro}"); // false

Console.WriteLine($"Wallet content: {string.Join(", ", wallet)}"); // Wallet content: 1.00 USD
contains = wallet.Contains(new Money(1.00m, new Currency("EUR"))); // false (always will be)
Console.WriteLine($"Wallet contains one euro: {contains}");        // false

DateOnly today = DateOnly.FromDateTime(DateTime.UtcNow);
DateOnly monthStart = new DateOnly(today.Year, today.Month, 1);
uint daysInMonth = (uint)DateTime.DaysInMonth(today.Year, today.Month);
Period accountingPeriod = new(monthStart, daysInMonth);

IInvoiceFactory invoiceFactory = new ValidatingInvoiceFactory(
    ServiceDateValidator.RegularInvoiceSubscriptionService(),
    IssueDateValidator.RegularInvoicePosting(accountingPeriod, today));

Company.IdType thisCompanyId = Company.NewId();
Company.IdType otherCompanyId = Company.NewId();
Invoice.IdType invoiceId = new(Guid.NewGuid());
ServiceDate serviceDate = new(DateOnly.FromDateTime(DateTime.UtcNow));
Currency currency = new("USD");

Company thisCompany = new(thisCompanyId, "This Company");
Company otherCompany = new(otherCompanyId, "Other Company");

Invoice draftInvoice = invoiceFactory.CreateDraft(
    invoiceId, thisCompany, otherCompany, serviceDate, currency);
