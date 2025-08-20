using Planity.FrontendBlazorWASM.Shared.Abstractions;

namespace Planity.FrontendBlazorWASM.Shared.Models;

public class Project : IGanttItem
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string Name { get; set; } = "";
    public string Description { get; set; } = "";
    public DateTime? Start => Tasks.Min(t => t.Start);
    public DateTime? End => Tasks.Max(t => t.End);
    public ProjectStatus Status { get; set; }
    public List<ProjectTask> Tasks { get; set; } = new();
    public List<Milestone> Milestones { get; set; } = new();
    public string? Color => null;
    public IEnumerable<IGanttItem>? Children => new IGanttItem[]{}.Concat(Tasks).Concat(Milestones);
    public bool IsExpanded { get; set; } = false;
    public IEnumerable<IGanttItem>? Predecessors { get; set; } = [];
    public IEnumerable<IGanttItem>? Successors { get; set; } = [];

    GanttItemType IGanttItem.Type => GanttItemType.Project;
}
