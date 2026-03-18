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
Invoice draft = InitiateInvoice(invoiceFactory);
// Console.WriteLine(draft.GetType().Name);
// persist draft...

// ...
if (draft is DraftInvoice notIssuedYet)
{
    Invoice issued = IssueToday(notIssuedYet, new(Guid.NewGuid(), today.Year, 1));
    // Console.WriteLine(issued.GetType().Name);
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

Invoice InitiateInvoice(IInvoiceFactory invoiceFactory)
{
    Company company = new("Our preciousss client");
    Currency usd = new("USD");
    DateOnly today = DateOnly.FromDateTime(DateTime.UtcNow);
    int daysInMonth = DateTime.DaysInMonth(today.Year, today.Month);
    ServiceDate endOfMonth = new(new DateOnly(today.Year, today.Month, daysInMonth));

    Invoice service = invoiceFactory.CreateDraft(company, endOfMonth, usd);
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

    // RULE #4: PROTECT THE INVARIANTS
    item1.UnitPrice = new Money(1, new Currency("EUR"));

    Console.WriteLine($"Invoice ({draft.Currency}) to {draft.IssuedTo.Name}:");
    foreach (IReadOnlyInvoiceItem item in draft.Items)
    {
        Console.WriteLine($"- {item.Name}: {item.Quantity} x {item.UnitPrice} = {item.TotalPrice}");
    }
}