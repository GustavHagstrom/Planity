namespace Planity.FrontendBlazorWASM.Shared.Abstractions;

public interface IGanttItem
{
    string Name { get; }
    DateTime? Start { get; }
    DateTime? End { get; }
    GanttItemType Type { get; }     // t.ex. "Task", "Project"
    IEnumerable<IGanttItem>? Children { get; } // För projekt med tasks
    bool IsExpanded { get; set; }
}
