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
    public List<IGanttItem> Predecessors { get; set; } = new();
    public List<IGanttItem> Successors { get; set; } = new();

    GanttItemType IGanttItem.Type => GanttItemType.Project;
    IEnumerable<IGanttItem>? IGanttItem.Predecessors { get => Predecessors; set => Predecessors = value?.ToList() ?? new List<IGanttItem>(); }
    IEnumerable<IGanttItem>? IGanttItem.Successors { get => Successors; set => Successors = value?.ToList() ?? new List<IGanttItem>(); }
}
