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

    public DateTime? Start => throw new NotImplementedException();

    public DateTime? End => throw new NotImplementedException();

    public GanttItemType Type => throw new NotImplementedException();

    public string? Color => throw new NotImplementedException();

    public IEnumerable<IGanttItem>? Children => throw new NotImplementedException();

    public bool IsExpanded { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
}
