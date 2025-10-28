namespace Planity.FrontendBlazorWASM.Shared.Models;

public class WorkCalendar
{
    // Identifierare för resursen
    public string ResourceId { get; set; } = string.Empty;

    // Arbetspass per veckodag (t.ex. 08:00-12:00, 13:00-17:00)
    public HashSet<WorkPeriod> WorkPeriods = 
    [
        new WorkPeriod { Day = DayOfWeek.Monday, Start = TimeSpan.FromHours(8), End = TimeSpan.FromHours(16.5), BreakDuration = 1 },
        new WorkPeriod { Day = DayOfWeek.Tuesday, Start = TimeSpan.FromHours(8), End = TimeSpan.FromHours(16.5), BreakDuration = 1 },
        new WorkPeriod { Day = DayOfWeek.Wednesday, Start = TimeSpan.FromHours(8), End = TimeSpan.FromHours(16.5), BreakDuration = 1 },
        new WorkPeriod { Day = DayOfWeek.Thursday, Start = TimeSpan.FromHours(8), End = TimeSpan.FromHours(16.5), BreakDuration = 1 },
        new WorkPeriod { Day = DayOfWeek.Friday, Start = TimeSpan.FromHours(8), End = TimeSpan.FromHours(14), BreakDuration = 1 }
    ];
    // Special-helgdagar (t.ex. påsk, midsommar)
    public HashSet<Holyday> Holidays { get; set; } = [];
    // Övertid per dag (datum -> antal extra timmar)
    public HashSet<Overtime> Overtime { get; set; } = [];
}