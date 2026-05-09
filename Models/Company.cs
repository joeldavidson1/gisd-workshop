namespace Gisd.Models;

public class Company(Company.IdType id, string name)
{
    public readonly record struct IdType(Guid Value);

    public static IdType NewId() => new(Guid.NewGuid());
    
    public IdType Id { get; } = id;

    public string Name
    {
        get => field;
        set => field =
            !string.IsNullOrWhiteSpace(value) ? value
            : throw new ArgumentException("CustomerName cannot be null or whitespace.");
    } = name;
}
// Test