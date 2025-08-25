using Planity.FrontendBlazorWASM.Shared.Abstractions;
using Planity.FrontendBlazorWASM.Shared.Models;

namespace Planity.FrontendBlazorWASM.Shared.Services;

public interface IGanttDateCalculator
{
    Task<DateTime?> CalculateEndAsync(IGanttItem item);
    DateTime? CalculateEnd(IGanttItem item, List<Resource> resources);
}
