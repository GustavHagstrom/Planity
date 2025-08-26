namespace Planity.FrontendBlazorWASM.Shared.Models;

public class WorkCalendar
{
    // Identifierare för resursen
    public string ResourceId { get; set; } = string.Empty;

    // Arbetspass per veckodag (t.ex. 08:00-12:00, 13:00-17:00)
    private readonly Dictionary<DayOfWeek, List<(TimeSpan Start, TimeSpan End)>> _workPeriodsByDayOfWeek = new()
    {
        { DayOfWeek.Monday,    [ (TimeSpan.FromHours(8), TimeSpan.FromHours(16.5)) ] }, // 08:00-16:30
        { DayOfWeek.Tuesday,   [ (TimeSpan.FromHours(8), TimeSpan.FromHours(16.5)) ] },
        { DayOfWeek.Wednesday, [ (TimeSpan.FromHours(8), TimeSpan.FromHours(16.5)) ] },
        { DayOfWeek.Thursday,  [ (TimeSpan.FromHours(8), TimeSpan.FromHours(16.5)) ] },
        { DayOfWeek.Friday,    [ (TimeSpan.FromHours(8), TimeSpan.FromHours(14)) ] },   // 08:00-14:00
        { DayOfWeek.Saturday,  [] },
        { DayOfWeek.Sunday,    [] }
    };
    public IReadOnlyDictionary<DayOfWeek, List<(TimeSpan Start, TimeSpan End)>> WorkPeriodsByDayOfWeek => _workPeriodsByDayOfWeek;

    public void SetWorkPeriodsByDayOfWeek(DayOfWeek day, List<(TimeSpan Start, TimeSpan End)> periods)
    {
        _workPeriodsByDayOfWeek[day] = periods;
    }

    // Special-helgdagar (t.ex. påsk, midsommar)
    public HashSet<DateTime> Holidays { get; set; } = new();
    // Övertid per dag (datum -> antal extra timmar)
    public Dictionary<DateTime, double> OvertimeHours { get; set; } = new();
}