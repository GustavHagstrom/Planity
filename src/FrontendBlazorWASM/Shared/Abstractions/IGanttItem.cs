namespace Planity.FrontendBlazorWASM.Shared.Abstractions;

public interface IGanttItem
{
    string Name { get; set;  }
    DateTime? Start { get; set; }
    GanttItemType Type { get; }     // t.ex. "Task", "Project"
    IEnumerable<IGanttItem>? Children { get; } // För projekt med tasks
    bool IsExpanded { get; set; }
    // Beroenden
    List<IGanttItem> Predecessors { get; set; } // Items som måste vara klara innan detta kan starta
    List<IGanttItem> Successors { get; set; }   // Items som startar efter detta är klart
}
