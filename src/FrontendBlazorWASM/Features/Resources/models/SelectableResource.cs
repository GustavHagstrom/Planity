using Planity.FrontendBlazorWASM.Shared.Models;

namespace Planity.FrontendBlazorWASM.Features.Resources.models;

public class SelectableResource : Resource
{
    public SelectableResource(Resource resource)
    {
        this.Id = resource.Id;
        this.Name = resource.Name;
        this.OrganizationId = resource.OrganizationId;
        this.Workers = resource.Workers;
        this.Efficiency = resource.Efficiency;
        this.WorkCalendar = resource.WorkCalendar;
    }
    public bool IsSelected { get; set; } = false;   
}
