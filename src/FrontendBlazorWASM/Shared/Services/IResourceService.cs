using Planity.FrontendBlazorWASM.Shared.Models;

namespace Planity.FrontendBlazorWASM.Shared.Services;

public interface IResourceService
{
    Task<List<Resource>> GetResourcesAsync();
    Task<Resource?> GetResourceByIdAsync(string resourceId);
    Task<Resource> CreateResourceAsync(Resource resource);
    Task<Resource> UpdateResourceAsync(Resource resource);
    Task DeleteResourceAsync(string resourceId);
}
