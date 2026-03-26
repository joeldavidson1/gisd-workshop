using Gisd.Models.Common;

// MONAD

// Operations:
// 1. Unit: A -> M A
// 2. Bind: M A -> (A -> M B) -> M B
// 3. Map: M A -> (A -> B) -> M B (in terms of Unit and Bind: Map m f = Bind m (x => Unit (f x)))

// Example:

Func<string, string> transform = s =>
    s.NullableUnit().BindNullable(toUpper).MapNullable(explain).MatchNullable(s => s, () => "<null>");

Console.WriteLine(transform("Hello, World!"));
Console.WriteLine(transform("hi, there!"));
Console.WriteLine(transform(""));

IEnumerable<string> lines = ["Hello, World!", "hi, there!", ""];
string report = lines.Bind(s => s.Split()).Map(transform).Join(Environment.NewLine);

Console.WriteLine();
Console.WriteLine(report);

string? toUpper(string str) =>
    str == string.Empty ? string.Empty
    : char.IsLower(str[0]) ? null
    : str.ToUpper();

string explain(string str) =>
    str == string.Empty ? "<empty>"
    : $"{str.Length} characters";