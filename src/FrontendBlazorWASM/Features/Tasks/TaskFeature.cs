using Microsoft.Extensions.Localization;
using Planity.FrontendBlazorWASM.Features.Resources;
using Planity.FrontendBlazorWASM.Shared.Abstractions;

namespace Planity.FrontendBlazorWASM.Features.Tasks;

public class TaskFeature(IStringLocalizer<TaskFeature> Localizer) : IFeature
{
    public void RegisterRenderObjects(RenderCollection collection)
    {
        collection.AddAppBarNavigationComponent<AppbarNavigation>();
        collection.AddCreateNewLink(new MenuLink(Localizer["Uppgift"], Routes.TaskNew));
    }

    public void RegisterServices(IServiceCollection services)
    {

    }
}
