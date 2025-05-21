using Planity.FrontendBlazorWASM.Features.Tasks;

namespace Planity.FrontendBlazorWASM.Features.Project;

public class Project
{
    public Guid Id { get; set; }
    public string Name { get; set; } = "";
    public string Description { get; set; } = "";
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public ProjectStatus Status { get; set; }

    public List<ProjectTask> Tasks { get; set; } = new();
}
