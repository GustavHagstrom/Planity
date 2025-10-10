using Planity.FrontendBlazorWASM.Shared.Abstractions;

namespace Planity.FrontendBlazorWASM.Features.Project;

public class ProjectFeature : IFeature
{
    public void RegisterInjecableComponents(ComponentTypeCollection typeCollection)
    {
        typeCollection.AddAppBarNavigationComponent<AppbarNavigation>();
    }
    public void RegisterServices(IServiceCollection services)
    {

    }
}
