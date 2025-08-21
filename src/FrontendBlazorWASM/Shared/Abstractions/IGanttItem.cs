namespace Planity.FrontendBlazorWASM.Shared.Abstractions;

public interface IGanttItem
{
    string Name { get; }
    DateTime? Start { get; }
    DateTime? End { get; }
    GanttItemType Type { get; }     // t.ex. "Task", "Project"
    IEnumerable<IGanttItem>? Children { get; } // För projekt med tasks
    bool IsExpanded { get; set; }

    // Beroenden
    IEnumerable<IGanttItem>? Predecessors { get; set; } // Items som måste vara klara innan detta kan starta
    IEnumerable<IGanttItem>? Successors { get; set; }   // Items som startar efter detta är klart
}
