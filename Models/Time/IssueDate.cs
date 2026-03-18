namespace Gisd.Models.Time;

public record struct IssueDate(DateOnly Value)
{
    public static implicit operator DateOnly(IssueDate serviceOn) =>
        serviceOn.Value;
}