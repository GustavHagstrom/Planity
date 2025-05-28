namespace Planity.FrontendBlazorWASM.Shared.Models;

public class Milesonte
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string OrganizationId { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;

    public DateTime Date { get; set; }

    // Koppling till projekt (frivilligt beroende på struktur)
    public Guid ProjectId { get; set; }

    // Statusindikator om den är uppnådd
    public bool IsCompleted { get; set; } = false;
}
