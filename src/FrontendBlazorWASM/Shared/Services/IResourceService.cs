using Planity.FrontendBlazorWASM.Shared.Models;

namespace Planity.FrontendBlazorWASM.Shared.Services;

public interface IResourceService
{
    Task<List<Resource>> GetAllAsync();
    Task<List<Resource>> GetAllAsync(List<string> ids);
    Task<Resource?> GetAsync(string resourceId);
    Task CreateAsync(Resource resource);
    Task UpdateAsync(Resource resource);
    Task DeleteAsync(string resourceId);
}
