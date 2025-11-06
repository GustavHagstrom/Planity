using Planity.FrontendBlazorWASM.Shared.Models;
using Planity.FrontendBlazorWASM.Shared.Mock;

namespace Planity.FrontendBlazorWASM.Shared.Services;

public class MockResourceService : IResourceService
{
    public Task<Resource?> GetAsync(string resourceId)
    {
        var resource = MockedDataStore.Resources.FirstOrDefault(r => r.Id == resourceId);
        if (resource is not null)
        {
            var tasks = MockedDataStore.Tasks.Where(t => t.ResourceId == resourceId).ToList();
            resource.Tasks = tasks;
        }
        return Task.FromResult(MockedDataStore.Resources.FirstOrDefault(r => r.Id == resourceId));
    }
        
    public Task<List<Resource>> GetAllAsync()
    {
        var resources = new List<Resource>();
        foreach (var resource in MockedDataStore.Resources)
        {
            var tasks = MockedDataStore.Tasks.Where(t => t.ResourceId == resource.Id).ToList();
            resource.Tasks = tasks;
            resources.Add(resource);
        }
        return Task.FromResult(resources);
    }
    public Task<List<Resource>> GetAllAsync(List<string> ids)
    {
        var resources = new List<Resource>();
        foreach (var resource in MockedDataStore.Resources.Where(r => ids.Contains(r.Id)))
        {
            var tasks = MockedDataStore.Tasks.Where(t => t.ResourceId == resource.Id).ToList();
            resource.Tasks = tasks;
            resources.Add(resource);
        }
        return Task.FromResult(resources);
    }

    public Task CreateAsync(Resource resource)
    {
        MockedDataStore.Resources.Add(resource);
        return Task.CompletedTask;
    }

    public Task UpdateAsync(Resource resource)
    {
        var existing = MockedDataStore.Resources.FirstOrDefault(r => r.Id == resource.Id);
        if (existing != null)
        {
            MockedDataStore.Resources.Remove(existing);
        }
        MockedDataStore.Resources.Add(resource);
        return Task.CompletedTask;

    }

    public Task DeleteAsync(string resourceId)
    {
        var existing = MockedDataStore.Resources.FirstOrDefault(r => r.Id == resourceId);
        if (existing != null)
        {
            MockedDataStore.Resources.Remove(existing);
        }
        return Task.CompletedTask;
    }

    

}
