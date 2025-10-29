namespace Planity.FrontendBlazorWASM.Shared.Models;

public class Vacation
{
    public DateOnly From { get; set; } = DateOnly.FromDateTime(DateTime.Now.Date);
    public DateOnly To { get; set; } = DateOnly.FromDateTime(DateTime.Now.AddDays(7));

}
