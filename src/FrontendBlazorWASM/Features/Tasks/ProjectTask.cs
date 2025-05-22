namespace Planity.FrontendBlazorWASM.Features.Tasks;

public class ProjectTask
{
    public Guid Id { get; set; }
    public string Name { get; set; } = "";
    public string? Description { get; set; }
    public DateTime Start { get; set; }
    public DateTime End { get; set; }
    public TaskStatus Status { get; set; }
    public string AssignedResourceId { get; set; } = string.Empty;
    public string AssignedResourceName { get; set; } = string.Empty;
}
