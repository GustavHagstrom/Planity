using Planity.FrontendBlazorWASM.Shared.Models;

namespace Planity.FrontendBlazorWASM.Shared.Services;

public class MockResourceService : IResourceService
{
    private readonly List<Resource> _resources = new()
    {
        new Resource { Id = "1", Name = "Anna Andersson", Role = "Projektledare", OrganizationId = "Org1" },
        new Resource { Id = "2", Name = "Bertil Berg", Role = "Snickare", OrganizationId = "Org1" },
        new Resource { Id = "3", Name = "Cecilia Carlsson", Role = "Elektriker", OrganizationId = "Org1" }
    };

    public Task<List<Resource>> GetResourcesAsync() => Task.FromResult(_resources.ToList());

    public Task<Resource?> GetResourceByIdAsync(string resourceId) =>
        Task.FromResult(_resources.FirstOrDefault(r => r.Id == resourceId));

    public Task<Resource> CreateResourceAsync(Resource resource)
    {
        resource.Id = Guid.NewGuid().ToString();
        _resources.Add(resource);
        return Task.FromResult(resource);
    }

    public Task<Resource> UpdateResourceAsync(Resource resource)
    {
        var existing = _resources.FirstOrDefault(r => r.Id == resource.Id);
        if (existing != null)
        {
            existing.Name = resource.Name;
            existing.Role = resource.Role;
            existing.RoleId = resource.RoleId;
            existing.OrganizationId = resource.OrganizationId;
        }
        return Task.FromResult(resource);
    }

    public Task DeleteResourceAsync(string resourceId)
    {
        var resource = _resources.FirstOrDefault(r => r.Id == resourceId);
        if (resource != null)
            _resources.Remove(resource);
        return Task.CompletedTask;
    }
}
