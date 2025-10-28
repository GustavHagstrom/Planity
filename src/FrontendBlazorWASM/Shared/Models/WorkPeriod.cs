namespace Planity.FrontendBlazorWASM.Shared.Models;

public class WorkPeriod : IEquatable<WorkPeriod>
{
    public DayOfWeek Day { get; set; }
    public TimeSpan Start { get; set; }
    public TimeSpan End { get; set; }
    public double BreakDuration { get; set; } // i timmar

    public bool Equals(WorkPeriod? other)
    {
        if (other is null)
            return false;
        return Day == other.Day &&
               Start == other.Start &&
               End == other.End &&
               BreakDuration == other.BreakDuration;
    }
}
