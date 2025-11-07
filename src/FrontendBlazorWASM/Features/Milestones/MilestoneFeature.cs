using Microsoft.Extensions.Localization;
using Planity.FrontendBlazorWASM.Shared.Abstractions;

namespace Planity.FrontendBlazorWASM.Features.Milestones;

public class MilestoneFeature(IStringLocalizer<MilestoneFeature> Localizer) : IFeature
{
    public void RegisterRenderObjects(RenderCollection collection)
    {
        collection.AddAppBarNavigationComponent<AppbarNavigation>();
        collection.AddCreateNewLink(new MenuLink(Localizer["Milsten"], Routes.MilestoneNew()));
    }
    public void RegisterServices(IServiceCollection services)
    {

    }
}
