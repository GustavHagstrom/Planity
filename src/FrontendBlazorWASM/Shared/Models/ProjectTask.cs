using Planity.FrontendBlazorWASM.Shared.Abstractions;

namespace Planity.FrontendBlazorWASM.Shared.Models;

public class ProjectTask : IGanttItem
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string OrganizationId { get; set; } = string.Empty;
    public string Name { get; set; } = "";
    public string? Description { get; set; }
    public DateTime? Start { get; set; }
    public DateTime? End { get; set; }
    public ProjectTaskStatus Status { get; set; }
    public string AssignedResourceId { get; set; } = string.Empty;
    public string AssignedResourceName { get; set; } = string.Empty;

    public GanttItemType Type => GanttItemType.Task;

    public string? Color => null;

    public IEnumerable<IGanttItem>? Children => [];

    public bool IsExpanded { get; set; } = false;

    public IEnumerable<IGanttItem>? Predecessors { get; set; } = [];
    public IEnumerable<IGanttItem>? Successors { get; set; } = [];
}