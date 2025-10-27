using Microsoft.Extensions.Localization;
using Planity.FrontendBlazorWASM.Features.Project.Services;
using Planity.FrontendBlazorWASM.Shared.Abstractions;

namespace Planity.FrontendBlazorWASM.Features.Project;

public class ProjectFeature(IStringLocalizer<ProjectFeature> Localizer) : IFeature
{
    public void RegisterRenderObjects(RenderCollection collection)
    {
        collection.AddAppBarNavigationComponent<AppbarNavigation>();
        collection.AddCreateNewLink(new MenuLink(Localizer["Projekt"], Routes.ProjectsNew));
    }
    public void RegisterServices(IServiceCollection services)
    {
        services.AddScoped<ProjectFactory>();
        services.AddScoped<SelectedProjectsState>();
        services.AddScoped<ProjectNavigator>();
    }
}
