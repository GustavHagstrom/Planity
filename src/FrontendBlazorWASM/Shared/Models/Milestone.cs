using Planity.FrontendBlazorWASM.Shared.Abstractions;

namespace Planity.FrontendBlazorWASM.Shared.Models;

public class Milestone : IGanttItem
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string OrganizationId { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public Guid ProjectId { get; set; }
    public DateTime? Start { get; set; }
    public GanttItemType Type => GanttItemType.Milestone;
    public IEnumerable<IGanttItem>? Children => null;
    public bool IsExpanded { get => false; set { } }
    public List<IGanttItem> Predecessors { get; set; } = [];
    public List<IGanttItem> Successors { get; set; } = [];
}
