using Planity.FrontendBlazorWASM.Shared.Abstractions;

namespace Planity.FrontendBlazorWASM.Shared.Models;

public class Resource : IGanttItem
{
    public string Id { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string? RoleId { get; set; } 
    public string? Role { get; set; } // t.ex. Snickare, Projektledare

    public DateTime? Start => throw new NotImplementedException();

    public DateTime? End => throw new NotImplementedException();

    public GanttItemType Type => throw new NotImplementedException();

    public string? Color => throw new NotImplementedException();

    public IEnumerable<IGanttItem>? Children => throw new NotImplementedException();

    public bool IsExpanded { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
}
