using Planity.FrontendBlazorWASM.Shared.Abstractions;
using Planity.FrontendBlazorWASM.Shared.Models;

namespace Planity.FrontendBlazorWASM.Shared.Services;

public interface IDateCalculator
{
    Task<DateTime?> CalculateEndAsync(IGanttItem item);
    DateTime? CalculateEnd(IGanttItem item, List<Resource> resources);
    bool IsWorkDay(WorkCalendar calendar, DateTime date);
    DateTime? CalculateStart(IGanttItem task, List<Resource> resources);
}
