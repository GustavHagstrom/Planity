using Microsoft.Extensions.Localization;
using Planity.FrontendBlazorWASM.Features.Resources.Services;
using Planity.FrontendBlazorWASM.Shared.Abstractions;

namespace Planity.FrontendBlazorWASM.Features.Resources;

public class ResourceFeature(IStringLocalizer<ResourceFeature> Localizer) : IFeature
{
    public void RegisterRenderObjects(RenderCollection collection)
    {
        collection.AddAppBarNavigationComponent<AppbarNavigation>();
        collection.AddCreateNewLink(new MenuLink(Localizer["Resurs"], Routes.ResourceNew));
    }

    public void RegisterServices(IServiceCollection services)
    {
        services.AddScoped<ResourceNavigator>();
        services.AddScoped<SelectedResourcesState>();
    }
}
