using Microsoft.AspNetCore.Components;

namespace Planity.FrontendBlazorWASM.Shared.Abstractions;

public interface IFeature
{
    void RegisterServices(IServiceCollection services);
    void RegisterRenderObjects(RenderCollection collection);
}
