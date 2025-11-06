using Planity.FrontendBlazorWASM.Shared.Abstractions;
using Planity.FrontendBlazorWASM.Shared.Models;

namespace Planity.FrontendBlazorWASM.Shared.Models;

public class Resource : IGanttItem
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string OrganizationId { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public int Workers { get; set; } = 1; // Antal personer i resursen
    public double Efficiency { get; set; } = 1.0; // Effektivitetsfaktor, t.ex. 1.0 = normal, 1.2 = 20% mer effektiv
    public List<ProjectTask> Tasks { get; set; } = new List<ProjectTask>();
    public DateTime? Start { get => null; set { } } // Kalkylerad, ej setbar
    public GanttItemType Type => GanttItemType.Resource;
    public IEnumerable<IGanttItem>? Children => Tasks;
    public bool IsExpanded { get; set; } = false;
    // Koppling till WorkCalendar
    public WorkCalendar WorkCalendar { get; set; } = new WorkCalendar();

    public IGanttItem Clone()
    {
        var clone = new Resource
        {
            Id = this.Id,
            OrganizationId = this.OrganizationId,
            Name = this.Name,
            Workers = this.Workers,
            Efficiency = this.Efficiency,
            IsExpanded = this.IsExpanded,
            WorkCalendar = this.WorkCalendar
        };
        return clone;
    }

    public bool Equals(IGanttItem? other)
    {
        if (other is not Resource r)
            return false;
        return Id == r.Id &&
               OrganizationId == r.OrganizationId &&
               Name == r.Name &&
               Workers == r.Workers &&
               Efficiency == r.Efficiency &&
               IsExpanded == r.IsExpanded;
    }

    public override bool Equals(object? obj)
    {
        return Equals(obj as IGanttItem);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Id, OrganizationId, Name, Workers, Efficiency, IsExpanded);
    }
}
