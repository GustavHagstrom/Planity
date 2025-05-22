using Planity.FrontendBlazorWASM.Features.Dashboard;
using Planity.FrontendBlazorWASM.Features.Project;
using Planity.FrontendBlazorWASM.Features.Settings;
using Planity.FrontendBlazorWASM.Features.Shared;
using Planity.FrontendBlazorWASM.Features.Tasks;

namespace Planity.FrontendBlazorWASM;

public static class FeatureServiceRegistration
{
    public static IServiceCollection AddFeatureServices(this IServiceCollection services)
    {
        services.AddSharedServices();
        services.AddDashboardServices();
        services.AddProjectServices();
        services.AddTaskServices();
        services.AddSettingsServices();
        return services;
    }
}
