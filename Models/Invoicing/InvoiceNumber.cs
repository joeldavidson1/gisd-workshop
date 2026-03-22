using Gisd.Models.Common;
using Gisd.Models.Time;

namespace Gisd.Models.Invoicing;

public class InvoiceNumber(Company.IdType companyId, int year, int sequence)
{
    public Company.IdType IssuingCompanyId
    {
        get => field;
        set => field =
            value.Value != Guid.Empty ? value
            : throw new ArgumentException("IssuingCompanyId cannot be empty.");
    } = companyId;
    
    public int Year
    {
        get => field;
        set => field =
            value > 0 ? value
            : throw new ArgumentException("InvoiceYear must be a positive integer.");
    } = year;
   
    public int SequenceNumber
    {
        get => field;
        set => field =
            value > 0 ? value
            : throw new ArgumentException("SequenceNumber must be a positive integer.");
    } = sequence;
}
