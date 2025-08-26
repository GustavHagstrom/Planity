using Planity.FrontendBlazorWASM.Shared.Models;
using Planity.FrontendBlazorWASM.Shared.Mock;

namespace Planity.FrontendBlazorWASM.Shared.Services;

public class MockResourceService(IAuthService authService) : IResourceService
{
    public Task<List<Resource>> GetResourcesAsync() => Task.FromResult(MockedDataStore.Resources.Values.ToList());

    public Task<Resource?> GetResourceByIdAsync(string resourceId) =>
        Task.FromResult(MockedDataStore.Resources.TryGetValue(resourceId, out var r) ? r : null);

    public Task<Resource> CreateResourceAsync(Resource resource)
    {
        resource.Id = Guid.NewGuid().ToString();
        MockedDataStore.Resources[resource.Id] = resource;
        return Task.FromResult(resource);
    }

    public Task<Resource> UpdateResourceAsync(Resource resource)
    {
        if (MockedDataStore.Resources.TryGetValue(resource.Id, out var existing))
        {
            existing.Name = resource.Name;
            existing.OrganizationId = resource.OrganizationId;
        }
        return Task.FromResult(resource);
    }

    public Task DeleteResourceAsync(string resourceId)
    {
        MockedDataStore.Resources.Remove(resourceId);
        return Task.CompletedTask;
    }

    public async Task<List<Resource>> GetOrganizationResources()
    {
        var orgId = await authService.GetOrganizationIdAsync();
        var resources = MockedDataStore.Resources.Values.Where(r => r.OrganizationId == orgId).ToList();
        return resources;
    }

    public async Task<List<ProjectTask>> GetTasksForResourceAsync(string resourceId)
    {
        // Samla alla tasks från alla projekt där ResourceId matchar
        var tasks = MockedDataStore.Projects
            .SelectMany(p => p.Tasks)
            .Where(t => t.ResourceId == resourceId)
            .ToList();
        return await Task.FromResult(tasks);
    }
}
