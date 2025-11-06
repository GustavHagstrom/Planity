using Planity.FrontendBlazorWASM.Shared.Mock;
using Planity.FrontendBlazorWASM.Shared.Models;

namespace Planity.FrontendBlazorWASM.Shared.Services;

public class MockedMilestoneService : IMilestoneService
{
    public Task CreateAsync(Milestone milestone)
    {
        MockedDataStore.Milestones.Add(milestone);
        return Task.CompletedTask;
    }

    public Task DeleteAsync(string milestoneId)
    {
        var milestone = MockedDataStore.Milestones.FirstOrDefault(m => m.Id == milestoneId);
        if (milestone != null)
        {
            MockedDataStore.Milestones.Remove(milestone);
        }
        return Task.CompletedTask;
    }

    public Task<List<Milestone>> GetAllAsync()
    {
        return Task.FromResult(MockedDataStore.Milestones);
    }

    public Task<List<Milestone>> GetAllAsync(IEnumerable<string> ids)
    {
        var milestones = MockedDataStore.Milestones.Where(m => ids.Contains(m.Id)).ToList();
        return Task.FromResult(milestones);
    }

    public Task<Milestone?> GetAsync(string milestoneId)
    {
        var milestone = MockedDataStore.Milestones.FirstOrDefault(m => m.Id == milestoneId);
        return Task.FromResult(milestone);
    }

    public Task UpdateAsync(Milestone milestone)
    {
        var existingMilestone = MockedDataStore.Milestones.FirstOrDefault(m => m.Id == milestone.Id);
        if (existingMilestone != null)
        {
            MockedDataStore.Milestones.Remove(existingMilestone);
            MockedDataStore.Milestones.Add(milestone);
        }
        return Task.CompletedTask;
    }
}
