namespace Planity.FrontendBlazorWASM.Shared.Models;

public class Overtime
{
    public DateOnly Date { get; set; }
    public TimeSpan From { get; set; }
    public TimeSpan To { get; set; }
    public double TotalHours => (To - From).TotalHours;
}
