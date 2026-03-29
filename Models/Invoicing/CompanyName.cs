namespace Gisd.Models.Invoicing.Documenting;

public record CompanyName(CompanyName.IdType Id, string Value)
{
    public readonly record struct IdType(Guid Value);
}