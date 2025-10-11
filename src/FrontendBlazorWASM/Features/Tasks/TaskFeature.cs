using Planity.FrontendBlazorWASM.Shared.Abstractions;

namespace Planity.FrontendBlazorWASM.Features.Tasks;

public class TaskFeature : IFeature
{
    public void RegisterInjecableComponents(ComponentTypeCollection collection)
    {
        collection.AddAppBarNavigationComponent<AppbarNavigation>();
    }

    public void RegisterServices(IServiceCollection services)
    {

    }
}
