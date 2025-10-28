
namespace Planity.FrontendBlazorWASM.Features.Project.Models;

public class EndDateProject 
{
    public EndDateProject(Shared.Models.Project baseProject, DateTime? endDate)
    {
        Id = baseProject.Id;
        Name = baseProject.Name;
        Description = baseProject.Description;
        Start = baseProject.Start;
        End = endDate;
    }

    public string Id { get; private set; }
    public bool IsSelected { get; set; } = false;
    public string Name { get; private set; }
    public string Description { get; private set; }
    public DateTime? Start { get; private set; }
    public DateTime? End { get; private set; }
}
