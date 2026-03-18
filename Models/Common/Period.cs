namespace Gisd.Models.Common;

public record struct Period(DateOnly From, uint DaysCount)
{
    public DateOnly To => From.AddDays((int)DaysCount);

    public bool Contains(DateOnly date) =>
        From <= date && To >= date;

    public static Period FromDates(DateOnly from, DateOnly to) =>
        to < from ? new Period(from, (uint)DaysBetween(from, to))
        : throw new ArgumentException("Invalid date period");

    private static int DaysBetween(DateOnly a, DateOnly b) =>
        (ToDate(a) - ToDate(b)).Days;

    private static DateTime ToDate(DateOnly value) =>
        value.ToDateTime(TimeOnly.MinValue);
}