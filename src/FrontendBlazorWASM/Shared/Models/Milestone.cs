using Planity.FrontendBlazorWASM.Shared.Abstractions;

namespace Planity.FrontendBlazorWASM.Shared.Models;

public class Milestone : IGanttItem
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string OrganizationId { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;

    public DateTime? Date { get; set; }

    // Koppling till projekt (frivilligt beroende på struktur)
    public Guid ProjectId { get; set; }

    // Statusindikator om den är uppnådd
    public bool IsCompleted { get; set; } = false;

    public DateTime? Start => Date;

    public DateTime? End => Date;

    public GanttItemType Type => GanttItemType.Milestone;

    public IEnumerable<IGanttItem>? Children => null;

    public bool IsExpanded { get; set; }

    public IEnumerable<IGanttItem>? Predecessors { get; set; } = [];
    public IEnumerable<IGanttItem>? Successors { get; set; } = [];
}
