namespace Planity.FrontendBlazorWASM.Features.Resources;

public class SelectedResourcesState
{
    public List<string> Ids { get; set; } = new();
    public bool Expanded { get; set; } = false;
  
}
