using Microsoft.AspNetCore.Components;

namespace Planity.FrontendBlazorWASM.Features.Project.Services;

public  class ProjectNavigator(SelectedProjectsState selectedProjectsState,
                               NavigationManager navigationManager)
{
    public void ShowInGantt(List<string> projectIds)
    {
        selectedProjectsState.ProjectIds = projectIds;
        navigationManager.NavigateTo(Routes.ProjectGantt);
    }
}
