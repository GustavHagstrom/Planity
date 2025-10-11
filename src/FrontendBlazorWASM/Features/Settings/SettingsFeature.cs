using Planity.FrontendBlazorWASM.Shared.Abstractions;

namespace Planity.FrontendBlazorWASM.Features.Settings;

public class SettingsFeature : IFeature
{
    public void RegisterInjecableComponents(ComponentTypeCollection collection)
    {
        collection.AddAppBarNavigationComponent<AppbarNavigation>();
    }

    public void RegisterServices(IServiceCollection services)
    {

    }
}
