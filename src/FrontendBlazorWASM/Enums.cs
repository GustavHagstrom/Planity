namespace Planity.FrontendBlazorWASM;

public enum ProjectStatus 
{ 
    NotStarted, 
    InProgress, 
    Completed, 
    OnHold 
}
public enum ProjectTaskStatus 
{ 
    NotStarted, 
    InProgress, 
    Completed, 
    Blocked 
}
public enum GanttItemType
{
    Task,
    Project,
    Milestone,
    Resource
}