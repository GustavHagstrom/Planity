using Planity.FrontendBlazorWASM.Shared.Abstractions;
using System.Text.Json;

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

    public IGanttItem Clone()
    {
        var clone = new Milestone
        {
            Id = this.Id,
            OrganizationId = this.OrganizationId,
            Name = this.Name,
            Description = this.Description,
            ProjectId = this.ProjectId,
            Start = this.Start
        };
        return clone;
    }

    public bool Equals(IGanttItem? other)
    {
        if (other is not Milestone m)
            return false;
        return Id == m.Id &&
               OrganizationId == m.OrganizationId &&
               Name == m.Name &&
               Description == m.Description &&
               ProjectId == m.ProjectId &&
               Nullable.Equals(Start, m.Start);
    }

    public override bool Equals(object? obj)
    {
        return Equals(obj as IGanttItem);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Id, OrganizationId, Name, Description, ProjectId, Start);
    }
}
