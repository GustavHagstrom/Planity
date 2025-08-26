using Planity.FrontendBlazorWASM.Shared.Abstractions;

namespace Planity.FrontendBlazorWASM.Shared.Models;

public class Project : IGanttItem
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string Name { get; set; } = "";
    public string Description { get; set; } = "";
    public DateTime? Start
    {
        get => Tasks.Select(t => t.Start)
            .Concat(Milestones.Select(m => m.Start))
            .Where(d => d.HasValue)
            .DefaultIfEmpty(null)
            .Min();
        set { }
    }
    public List<ProjectTask> Tasks { get; set; } = [];
    public List<Milestone> Milestones { get; set; } = [];
    public IEnumerable<IGanttItem>? Children => Array.Empty<IGanttItem>().Concat(Tasks).Concat(Milestones);
    public bool IsExpanded { get; set; } = false;
    GanttItemType IGanttItem.Type => GanttItemType.Project;
    public List<IGanttItem> Predecessors { get; set; } = [];
    public List<IGanttItem> Successors { get; set; } = [];
}
