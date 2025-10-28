namespace Planity.FrontendBlazorWASM.Shared.Models;

public class WorkPeriod
{
    public DayOfWeek Day { get; set; }
    public TimeSpan Start { get; set; }
    public TimeSpan End { get; set; }
    public double BreakDuration { get; set; } // i timmar
}
