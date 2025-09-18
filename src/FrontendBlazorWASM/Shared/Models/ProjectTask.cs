using Planity.FrontendBlazorWASM.Shared.Abstractions;
using Planity.FrontendBlazorWASM.Shared.Models;

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

    public IGanttItem Clone()
    {
        var clone = new ProjectTask
        {
            Id = this.Id,
            OrganizationId = this.OrganizationId,
            Name = this.Name,
            Description = this.Description,
            Start = this.Start,
            WorkHours = this.WorkHours,
            ResourceId = this.ResourceId,
            IsExpanded = this.IsExpanded
        };
        return clone;
    }

    public bool Equals(IGanttItem? other)
    {
        if (other is not ProjectTask t)
            return false;
        return Id == t.Id &&
               OrganizationId == t.OrganizationId &&
               Name == t.Name &&
               Description == t.Description &&
               Start == t.Start &&
               WorkHours == t.WorkHours &&
               ResourceId == t.ResourceId &&
               IsExpanded == t.IsExpanded;
    }

    public override bool Equals(object? obj)
    {
        return Equals(obj as IGanttItem);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Id, OrganizationId, Name, Description, Start, WorkHours, ResourceId, IsExpanded);
    }
}