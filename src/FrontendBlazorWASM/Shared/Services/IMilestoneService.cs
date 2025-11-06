using Planity.FrontendBlazorWASM.Shared.Models;

namespace Planity.FrontendBlazorWASM.Shared.Services;

public interface IMilestoneService
{
    Task CreateAsync(Milestone milestone);
    Task UpdateAsync( Milestone milestone);
    Task DeleteAsync(string milestoneId);
    Task<Milestone?> GetAsync(string milestoneId);
    Task<List<Milestone>> GetAllAsync();
    Task<List<Milestone>> GetAllAsync(IEnumerable<string> ids);
}
