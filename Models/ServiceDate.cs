namespace Gisd.Models;

public record struct ServiceDate(DateOnly Value)
{
    public static implicit operator DateOnly(ServiceDate serviceOn) =>
        serviceOn.Value;
}