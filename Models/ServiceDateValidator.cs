namespace Gisd.Models;

public delegate ServiceDate ServiceDateValidator(ServiceDate value);

public static class StandardServiceDateValidators
{
    extension(ServiceDateValidator)
    {
        public static ServiceDateValidator RegularInvoicePastService(DateOnly referenceDate) =>
            (serviceOn) =>
                serviceOn <= referenceDate ? serviceOn
                : throw new ArgumentException("Service date cannot be in the future.");
        
        public static ServiceDateValidator RegularInvoiceSubscriptionService() =>
            (serviceOn) => serviceOn;
        
        public static ServiceDateValidator AdvanceInvoiceService() =>
            serviceOn => serviceOn;
    }
}