namespace Planity.FrontendBlazorWASM.Shared.Models;

public class WorkCalendar
{
    // Identifierare för resursen
    public string ResourceId { get; set; } = string.Empty;
    // Arbetstimmar per veckodag (alltid sju dagar, endast ändra värde)
    private readonly Dictionary<DayOfWeek, double> _workHoursByDayOfWeek = new()
    {
        { DayOfWeek.Monday, 8.5 },
        { DayOfWeek.Tuesday, 8.5 },
        { DayOfWeek.Wednesday, 8.5 },
        { DayOfWeek.Thursday, 8.5 },
        { DayOfWeek.Friday, 6 },
        { DayOfWeek.Saturday, 0 },
        { DayOfWeek.Sunday, 0 }
    };
    public IReadOnlyDictionary<DayOfWeek, double> WorkHoursByDayOfWeek => _workHoursByDayOfWeek;
    public void SetWorkHoursByDayOfWeek(DayOfWeek day, double hours)
    {
        if (_workHoursByDayOfWeek.ContainsKey(day))
            _workHoursByDayOfWeek[day] = hours;
    }
    // Special-helgdagar (t.ex. påsk, midsommar)
    public HashSet<DateTime> Holidays { get; set; } = new();
    // Övertid per dag (datum -> antal extra timmar)
    public Dictionary<DateTime, double> OvertimeHours { get; set; } = new();

}