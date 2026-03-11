namespace Gisd.Models;

public class Company(string name)
{
    public string Name
    {
        get => field;
        set => field =
            !string.IsNullOrWhiteSpace(value) ? value
            : throw new ArgumentException("CustomerName cannot be null or whitespace.");
    } = name;
}
