namespace Gisd.Models;

public class Company(Guid id, string name)
{
    public Guid Id { get; } = id;

    public string Name
    {
        get => field;
        set => field =
            !string.IsNullOrWhiteSpace(value) ? value
            : throw new ArgumentException("CustomerName cannot be null or whitespace.");
    } = name;
}
