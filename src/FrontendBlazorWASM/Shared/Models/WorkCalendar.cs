namespace Planity.FrontendBlazorWASM.Shared.Models;

public class WorkCalendar
{
    // Identifierare f�r resursen
    public string ResourceId { get; set; } = string.Empty;
    // Arbetstimmar per veckodag (alltid sju dagar, endast �ndra v�rde)
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
    // Special-helgdagar (t.ex. p�sk, midsommar)
    public HashSet<DateTime> Holidays { get; set; } = new();
    // �vertid per dag (datum -> antal extra timmar)
    public Dictionary<DateTime, double> OvertimeHours { get; set; } = new();

}