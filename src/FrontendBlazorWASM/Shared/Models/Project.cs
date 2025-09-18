using Planity.FrontendBlazorWASM.Shared.Abstractions;
using Planity.FrontendBlazorWASM.Shared.Models;

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

    public IGanttItem Clone()
    {
        var clone = new Project
        {
            Id = this.Id,
            Name = this.Name,
            Description = this.Description,
            Tasks = this.Tasks?.Select(t => (ProjectTask)t.Clone()).ToList() ?? new List<ProjectTask>(),
            Milestones = this.Milestones?.Select(m => (Milestone)m.Clone()).ToList() ?? new List<Milestone>(),
            IsExpanded = this.IsExpanded
        };
        return clone;
    }

    public bool Equals(IGanttItem? other)
    {
        if (other is not Project p)
            return false;
        return Id == p.Id &&
               Name == p.Name &&
               Description == p.Description &&
               IsExpanded == p.IsExpanded;
    }

    public override bool Equals(object? obj)
    {
        return Equals(obj as IGanttItem);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Id, Name, Description, IsExpanded);
    }
}
