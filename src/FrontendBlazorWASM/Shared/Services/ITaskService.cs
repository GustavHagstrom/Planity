using Planity.FrontendBlazorWASM.Shared.Models;

namespace Planity.FrontendBlazorWASM.Shared.Services;

public interface ITaskService
{
    Task CreateAsync(ProjectTask task);
    Task<ProjectTask?> GetAsync(string taskId);
    Task<List<ProjectTask>> GetFromResourceIdAsync(string resourceId);
    Task<List<ProjectTask>> GetAllAsync();
    Task<List<ProjectTask>> GetAllAsync(IEnumerable<string> ids);
    Task<ProjectTask> UpdateAsync(ProjectTask task);
    Task DeleteAsync(string taskId);
    
    
}
