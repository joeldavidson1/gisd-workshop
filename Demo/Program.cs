using Gisd.Models;

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
Console.WriteLine(draft.GetType().Name);
// persist draft...

// ...
if (draft is DraftInvoice notIssuedYet)
{
    Invoice issued = IssueToday(notIssuedYet, new(Guid.NewGuid(), today.Year, 1));
    Console.WriteLine(issued.GetType().Name);
    // persist issued...
}

// ---------------
// Request handler

Invoice IssueToday(DraftInvoice invoice, InvoiceNumber nextNumber)
{
    IssueDate issueOn = new(DateOnly.FromDateTime(DateTime.UtcNow));

    // RULE #3: USE DESIGN PRINCIPLES TO MAKE USE OF TYPES SIMPLE AND SAFE
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

    // RULE #3 (here, too)
    Invoice service = invoiceFactory.CreateDraft(company, endOfMonth, usd);

    return service;
}