using Planity.FrontendBlazorWASM.Shared.Abstractions;

namespace Planity.FrontendBlazorWASM.Features.Project;

public class ProjectFeature : IFeature
{
    public void RegisterInjecableComponents(ComponentTypeCollection collection)
    {
        collection.AddAppBarNavigationComponent<AppbarNavigation>();
    }
    public void RegisterServices(IServiceCollection services)
    {

    }
}
