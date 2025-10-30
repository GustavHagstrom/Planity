using Microsoft.AspNetCore.Components;

namespace Planity.FrontendBlazorWASM.Features.Resources.Services;

public class ResourceNavigator(SelectedResourcesState selectedResourcesState,
                              NavigationManager navigationManager)
{
    public void ShowInGantt(List<string> resourceIds, bool expanded = false)
    {
        selectedResourcesState.Ids = resourceIds;
        selectedResourcesState.Expanded = expanded;
        navigationManager.NavigateTo(Routes.ResourceGantt);
    }
}