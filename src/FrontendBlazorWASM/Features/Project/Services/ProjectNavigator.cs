using Microsoft.AspNetCore.Components;

namespace Planity.FrontendBlazorWASM.Features.Project.Services;

public  class ProjectNavigator(SelectedProjectsState selectedProjectsState,
                               NavigationManager navigationManager)
{
    public void ShowInGantt(List<string> projectIds, bool expanded = false)
    {
        selectedProjectsState.ProjectIds = projectIds;
        selectedProjectsState.Expanded = expanded;
        navigationManager.NavigateTo(Routes.ProjectGantt);
    }
}
