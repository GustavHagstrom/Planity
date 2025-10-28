namespace Planity.FrontendBlazorWASM.Features.Project;

public class SelectedProjectsState
{
    public List<string> ProjectIds { get; set; } = [];
    public bool Expanded { get; set; } = false;
}
