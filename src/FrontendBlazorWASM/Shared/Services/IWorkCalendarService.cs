using Planity.FrontendBlazorWASM.Shared.Models;

namespace Planity.FrontendBlazorWASM.Shared.Services;

public interface IWorkCalendarService
{
    Task<WorkCalendar> GetWorkCalendarAsync(string resourceId);
    Task AddWorkPeriod(string resourceId, WorkPeriod period);
    Task AddWorkPeriods(string resourceId, IEnumerable<WorkPeriod> periods);
    Task AddHolidayAsync(string resourceId, Holyday holyday);
    Task RemoveHolidayAsync(string resourceId, Holyday holyday);
    Task SetOvertimeAsync(string resourceId, DateOnly date, double overtimeHours);
    Task RemoveOvertimeAsync(string resourceId, DateOnly date);
}
