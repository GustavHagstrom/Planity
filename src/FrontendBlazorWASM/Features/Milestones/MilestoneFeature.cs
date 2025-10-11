using Planity.FrontendBlazorWASM.Shared.Abstractions;

namespace Planity.FrontendBlazorWASM.Features.Milestones;

public class MilestoneFeature : IFeature
{
    public void RegisterInjecableComponents(ComponentTypeCollection collection)
    {
        collection.AddAppBarNavigationComponent<AppbarNavigation>();
    }
    public void RegisterServices(IServiceCollection services)
    {

    }
}
