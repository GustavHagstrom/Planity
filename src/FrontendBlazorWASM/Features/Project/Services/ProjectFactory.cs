using Planity.FrontendBlazorWASM.Features.Project.Models;
using Planity.FrontendBlazorWASM.Shared.Models;
using Planity.FrontendBlazorWASM.Shared.Services;

namespace Planity.FrontendBlazorWASM.Features.Project.Services;

public class ProjectFactory
{
    private readonly IDateCalculator _dateCalculator;

    public ProjectFactory(IDateCalculator dateCalculator)
    {
        _dateCalculator = dateCalculator;
    }
    public EndDateProject CreateEndDateProject(Shared.Models.Project project, List<Resource> resources)
    {
        var endDate = _dateCalculator.CalculateEnd(project, resources);
        return new EndDateProject(project, endDate);
    }
}
