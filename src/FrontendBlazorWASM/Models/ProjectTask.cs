namespace Planity.FrontendBlazorWASM.Models;

public class ProjectTask
{
    public Guid Id { get; set; }
    public string Title { get; set; } = "";
    public string? Description { get; set; }
    public DateTime? DueDate { get; set; }
    public TaskStatus Status { get; set; }
    public string? AssignedTo { get; set; } // email eller användarnamn
}
