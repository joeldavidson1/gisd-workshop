namespace Gisd.Models;

public record struct IssueDate(DateOnly Value)
{
    public static implicit operator DateOnly(IssueDate serviceOn) =>
        serviceOn.Value;
}