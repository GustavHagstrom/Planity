using MudBlazor.Utilities.Clone;
using Planity.FrontendBlazorWASM.Shared.Models;

namespace Planity.FrontendBlazorWASM.Shared.Abstractions;

public interface IGanttItem : IEquatable<IGanttItem>
{
    string Id { get; set; }
    string Name { get; set;  }
    DateTime? Start { get; set; }
    GanttItemType Type { get; }     // t.ex. "Task", "Project"
    IEnumerable<IGanttItem>? Children { get; } // För projekt med tasks
    bool IsExpanded { get; set; }
    IGanttItem Clone();
}
