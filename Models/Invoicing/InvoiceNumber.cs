using Gisd.Models.Common;
using Gisd.Models.Time;

namespace Gisd.Models.Invoicing;

public class InvoiceNumber(Company.IdType CompanyId, int Year, int Sequence)
{
    public Company.IdType IssuingCompanyId
    {
        get => field;
        set => field =
            value.Value != Guid.Empty ? value
            : throw new ArgumentException("IssuingCompanyId cannot be empty.");
    } = CompanyId;
    
    public int Year
    {
        get => field;
        set => field =
            value > 0 ? value
            : throw new ArgumentException("InvoiceYear must be a positive integer.");
    } = Year;
   
    public int Sequence
    {
        get => field;
        set => field =
            value > 0 ? value
            : throw new ArgumentException("SequenceNumber must be a positive integer.");
    } = Sequence;
}
