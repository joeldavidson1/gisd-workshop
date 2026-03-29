using Gisd.Models.Time;
using Gisd.Models.Common;
using System.Collections.Immutable;

namespace Gisd.Models.Invoicing.Documenting;

// RULE #10 - SEPARATE RESPONSIBILITIES INTO DISTINCT CLASSES
public record InvoiceDocument(
    InvoiceDocument.IdType Id, Invoice.IdType InvoiceId, FinalInvoiceNumber Number, CompanyName IssuedBy, CompanyName IssuedTo,
    Address IssuedByAddress, Address IssuedToAddress,
    ServiceDate ServiceOn, IssueDate IssuedOn, ImmutableList<InvoiceLine> Items)
{
    public readonly record struct IdType(Guid Value);

    public static InvoiceDocument From(IssuedInvoice invoice, InvoiceNumberFormatter numberFormatter) =>
        throw new NotImplementedException("Mapping from IssuedInvoice to InvoiceDocument is not implemented yet.");
}
