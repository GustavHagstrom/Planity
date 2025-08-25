using Planity.FrontendBlazorWASM.Shared.Abstractions;

namespace Planity.FrontendBlazorWASM.Shared.Models;

public class Resource : IGanttItem
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string OrganizationId { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public int Capacity { get; set; } = 40; // Kapacitet i timmar per vecka, t.ex. 40 timmar
    public string? RoleId { get; set; } 
    public string? Role { get; set; } // t.ex. Snickare, Projektledare

    public DateTime? Start { get => null; set { } } // Kalkylerad, ej setbar

    public GanttItemType Type => GanttItemType.Resource;

    public string? Color => null;

    public IEnumerable<IGanttItem>? Children => null;

    public bool IsExpanded { get; set; } = false;

    public IEnumerable<IGanttItem>? Predecessors { get; set; } = [];
    public IEnumerable<IGanttItem>? Successors { get; set; } = [];
}
