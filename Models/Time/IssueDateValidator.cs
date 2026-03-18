using Gisd.Models.Common;

namespace Gisd.Models.Time;

public delegate IssueDate IssueDateValidator(ServiceDate serviceOn, IssueDate value);

public static class StandardIssueDateValidators
{
    extension(IssueDateValidator)
    {
        public static IssueDateValidator RegularInvoicePosting(Period accountingPeriod, DateOnly today) =>
            (_, issueOn) => 
                today > issueOn ? throw new ArgumentException("Posting date cannot be in the future")
                : !accountingPeriod.Contains(issueOn) ? throw new ArgumentException("Posting not allowed outside the accounting period")
                : issueOn;
        
        public static IssueDateValidator AdvanceInvoicePosting(Period accountingPeriod, DateOnly today) =>
            (serviceOn, issueOn) =>
                (DateOnly)issueOn > serviceOn ? throw new ArgumentException("Posting cannot happen after the service in an advance invoice")
                : RegularInvoicePosting(accountingPeriod, today).Invoke(serviceOn, issueOn);
    }
}