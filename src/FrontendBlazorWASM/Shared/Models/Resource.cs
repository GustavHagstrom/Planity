using Planity.FrontendBlazorWASM.Shared.Abstractions;

namespace Planity.FrontendBlazorWASM.Shared.Models;

public class Resource : IGanttItem
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string OrganizationId { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public int Workers { get; set; } = 1; // Antal personer i resursen
    public double Efficiency { get; set; } = 1.0; // Effektivitetsfaktor, t.ex. 1.0 = normal, 1.2 = 20% mer effektiv
    public DateTime? Start { get => null; set { } } // Kalkylerad, ej setbar
    public GanttItemType Type => GanttItemType.Resource;
    public IEnumerable<IGanttItem>? Children => null;
    public bool IsExpanded { get; set; } = false;
    public List<IGanttItem> Predecessors { get; set; } = [];
    public List<IGanttItem> Successors { get; set; } = [];

    // Koppling till WorkCalendar
    public WorkCalendar WorkCalendar { get; set; } = new WorkCalendar();
}
