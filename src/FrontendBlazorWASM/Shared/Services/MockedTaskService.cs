using Planity.FrontendBlazorWASM.Shared.Mock;
using Planity.FrontendBlazorWASM.Shared.Models;

namespace Planity.FrontendBlazorWASM.Shared.Services;

public class MockedTaskService : ITaskService
{
    public Task CreateAsync(ProjectTask task)
    {
        var tasks = MockedDataStore.Tasks;
        tasks.Add(task);
        return Task.CompletedTask;
    }

    public Task DeleteAsync(string taskId)
    {
        var task = MockedDataStore.Tasks.FirstOrDefault(t => t.Id == taskId);
        if (task != null)
        {
            MockedDataStore.Tasks.Remove(task);
        }
        return Task.CompletedTask;
    }

    public Task<List<ProjectTask>> GetAllAsync()
    {
        return Task.FromResult(MockedDataStore.Tasks);
    }

    public Task<List<ProjectTask>> GetAllAsync(IEnumerable<string> ids)
    {
        var tasks = MockedDataStore.Tasks.Where(t => ids.Contains(t.Id)).ToList();
        return Task.FromResult(tasks);
    }

    public Task<ProjectTask?> GetAsync(string taskId)
    {
        var task = MockedDataStore.Tasks.FirstOrDefault(t => t.Id == taskId);
        return Task.FromResult(task);
    }

    public Task<List<ProjectTask>> GetFromResourceIdAsync(string resourceId)
    {
        var task = MockedDataStore.Tasks.Where(t => t.ResourceId == resourceId).ToList();
        return Task.FromResult(task);
    }

    public Task<ProjectTask> UpdateAsync(ProjectTask task)
    {
        var existingTask = MockedDataStore.Tasks.FirstOrDefault(t => t.Id == task.Id);
        if (existingTask != null)
        {
            MockedDataStore.Tasks.Remove(existingTask);
            MockedDataStore.Tasks.Add(task);
        }
        return Task.FromResult(task);
    }
}
