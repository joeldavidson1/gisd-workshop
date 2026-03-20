namespace Gisd.Models.Common;

public class Currency(string symbol)
{
    public string Symbol
    {
        get => field;
        set => field =
            string.IsNullOrWhiteSpace(value) ? throw new ArgumentException("Currency cannot be null or whitespace.")
            : System.Text.RegularExpressions.Regex.IsMatch(value, @"^[A-Z]{3}$") ? value
            : throw new ArgumentException("Currency must be a valid ISO 4217 currency code.");
    } = symbol;

    public override string ToString() => Symbol;

    public override bool Equals(object? obj) =>
        obj is Currency other && Symbol == other.Symbol;
    
    public override int GetHashCode() =>
        Symbol.GetHashCode();
    
    public static bool operator ==(Currency left, Currency right) =>
        left.Equals(right);
    
    public static bool operator !=(Currency left, Currency right) =>
        !(left == right);
}
