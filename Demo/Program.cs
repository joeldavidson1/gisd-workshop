using Gisd.Models;
using Gisd.Models.Invoicing;
using Gisd.Models.Time;
using Gisd.Models.Common;

// Composition root

DateOnly today = DateOnly.FromDateTime(DateTime.UtcNow);
DateOnly monthStart = new DateOnly(today.Year, today.Month, 1);
uint daysInMonth = (uint)DateTime.DaysInMonth(today.Year, today.Month);
Period accountingPeriod = new(monthStart, daysInMonth);

IInvoiceFactory invoiceFactory = new ValidatingInvoiceFactory(
    ServiceDateValidator.RegularInvoiceSubscriptionService(),
    IssueDateValidator.RegularInvoicePosting(accountingPeriod, today));

// ...
Company issuer = new(Guid.NewGuid(), "Our preciousss");
Invoice draft = InitiateInvoice(issuer, invoiceFactory);
// persist draft...

// ...
if (draft is DraftInvoice notIssuedYet)
{
    Invoice issued = IssueToday(notIssuedYet, new(issuer.Id, today.Year, 1));
    // persist issued...
}

// ---------------
// Request handler

Invoice IssueToday(DraftInvoice invoice, InvoiceNumber nextNumber)
{
    IssueDate issueOn = new(DateOnly.FromDateTime(DateTime.UtcNow));

    Invoice issued = invoice.Issue(nextNumber, issueOn);

    return issued;
}

Invoice InitiateInvoice(Company issuer, IInvoiceFactory invoiceFactory)
{
    Company recipient = new(Guid.NewGuid(), "Our preciousss client");
    Currency usd = new("USD");
    DateOnly today = DateOnly.FromDateTime(DateTime.UtcNow);
    int daysInMonth = DateTime.DaysInMonth(today.Year, today.Month);
    ServiceDate endOfMonth = new(new DateOnly(today.Year, today.Month, daysInMonth));

    Invoice service = invoiceFactory.CreateDraft(issuer, recipient, endOfMonth, usd);
    if (service is DraftInvoice draft) AddItems(draft);

    return service;
}

void AddItems(DraftInvoice draft)
{
    Currency usd = new("USD");
    InvoiceItem item1 = new("Something", "Really, something", new Money(1, usd), 1);
    InvoiceItem item2 = new("Something", "Really, something", new Money(1, usd), 2);

    draft.Add(item1);
    draft.Add(item2);

    item1.UnitPrice = new Money(1, new Currency("EUR"));

    Console.WriteLine($"Invoice ({draft.Currency}) to {draft.IssuedTo.Name}:");
    foreach (IReadOnlyInvoiceItem item in draft.Items)
    {
        Console.WriteLine($"- {item.Name}: {item.Quantity} x {item.UnitPrice} = {item.TotalPrice}");
    }
}