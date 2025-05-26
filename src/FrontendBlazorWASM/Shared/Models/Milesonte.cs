namespace Planity.FrontendBlazorWASM.Shared.Models;

public class Milesonte
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;

    public DateTime Date { get; set; }

    // Koppling till projekt (frivilligt beroende på struktur)
    public Guid ProjectId { get; set; }

    // Statusindikator om den är uppnådd
    public bool IsCompleted { get; set; } = false;
}
