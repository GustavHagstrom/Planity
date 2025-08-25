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

    public DateTime? Start { get => Date; set => Date = value; }
    public DateTime? End { get => Date; set => Date = value; }

    public GanttItemType Type => GanttItemType.Milestone;

    public IEnumerable<IGanttItem>? Children => null;

    public bool IsExpanded { get; set; }

    public List<IGanttItem> Predecessors { get; set; } = new();
    public List<IGanttItem> Successors { get; set; } = new();

    IEnumerable<IGanttItem>? IGanttItem.Predecessors { get => Predecessors; set => Predecessors = value?.ToList() ?? new List<IGanttItem>(); }
    IEnumerable<IGanttItem>? IGanttItem.Successors { get => Successors; set => Successors = value?.ToList() ?? new List<IGanttItem>(); }
}
