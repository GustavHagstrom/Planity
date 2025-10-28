using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Planity.FrontendBlazorWASM.Shared.Models;
using Planity.FrontendBlazorWASM.Shared.Services;

namespace Planity.FrontendBlazorWASM.Tests.TestDoubles;

public class FakeResourceService : IResourceService
{
 private readonly List<Resource> _resources;
 private readonly Dictionary<string, List<ProjectTask>> _tasksByResource = new();

 public FakeResourceService(IEnumerable<Resource> resources, IEnumerable<ProjectTask>? tasks = null)
 {
 _resources = resources.ToList();
 if (tasks != null)
 {
 foreach (var t in tasks)
 {
 if (!_tasksByResource.TryGetValue(t.ResourceId, out var list))
 {
 list = new List<ProjectTask>();
 _tasksByResource[t.ResourceId] = list;
 }
 list.Add(t);
 }
 }
 }

 public Task<List<Resource>> GetResourcesAsync() => Task.FromResult(_resources.ToList());

 public Task<Resource?> GetResourceByIdAsync(string resourceId)
 {
 return Task.FromResult(_resources.FirstOrDefault(r => r.Id == resourceId));
 }

 public Task<List<Resource>> GetOrganizationResources()
 {
 // In tests we don't separate organizations, return all provided resources
 return Task.FromResult(_resources.ToList());
 }

 public Task<Resource> CreateResourceAsync(Resource resource)
 {
 _resources.Add(resource);
 return Task.FromResult(resource);
 }

 public Task<Resource> UpdateResourceAsync(Resource resource)
 {
 var idx = _resources.FindIndex(r => r.Id == resource.Id);
 if (idx >=0) _resources[idx] = resource;
 return Task.FromResult(resource);
 }

 public Task DeleteResourceAsync(string resourceId)
 {
 _resources.RemoveAll(r => r.Id == resourceId);
 _tasksByResource.Remove(resourceId);
 return Task.CompletedTask;
 }

 public Task<List<ProjectTask>> GetTasksForResourceAsync(string resourceId)
 {
 if (_tasksByResource.TryGetValue(resourceId, out var list))
 return Task.FromResult(list.ToList());
 return Task.FromResult(new List<ProjectTask>());
 }
}
