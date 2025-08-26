using Planity.FrontendBlazorWASM.Shared.Abstractions;

namespace Planity.FrontendBlazorWASM.Shared.Models;

public class ProjectTask : IGanttItem
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string OrganizationId { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public DateTime? Start { get; set; }
    public double WorkHours { get; set; } = 8; // Exempel: 8 timmar arbete för att slutföra uppgiften
    public string ResourceId { get; set; } = string.Empty;
    public GanttItemType Type => GanttItemType.Task;
    public IEnumerable<IGanttItem>? Children => [];
    public bool IsExpanded { get; set; } = false;
    public List<IGanttItem> Predecessors { get; set; } = [];
    public List<IGanttItem> Successors { get; set; } = [];
}