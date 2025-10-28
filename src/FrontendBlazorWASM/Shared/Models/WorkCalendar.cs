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
    //public IReadOnlyList<WorkPeriod> WorkPeriods => WorkPeriods;

    //public void AddWorkPeriod(WorkPeriod workPeriod)
    //{
    //    WorkPeriods.Add(workPeriod);
    //}
    //public void AddWorkPeriods(IEnumerable<WorkPeriod> workPeriods)
    //{
    //    WorkPeriods.AddRange(workPeriods);
    //}
    //public void RemoveWorkPeriod(WorkPeriod workPeriod)
    //{
    //    WorkPeriods.Remove(workPeriod);
    //}


    // Special-helgdagar (t.ex. påsk, midsommar)
    public HashSet<Holyday> Holidays { get; set; } = [];
    // Övertid per dag (datum -> antal extra timmar)
    public Dictionary<DateOnly, double> OvertimeHours { get; set; } = new();
}