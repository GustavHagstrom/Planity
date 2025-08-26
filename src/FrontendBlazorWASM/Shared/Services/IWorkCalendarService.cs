using Planity.FrontendBlazorWASM.Shared.Models;

namespace Planity.FrontendBlazorWASM.Shared.Services;

public interface IWorkCalendarService
{
    Task<WorkCalendar> GetWorkCalendarAsync(string resourceId);
    Task SetWorkHoursByDayOfWeekAsync(string resourceId, Dictionary<DayOfWeek, double> workHoursByDayOfWeek);
    Task AddHolidayAsync(string resourceId, DateTime holiday);
    Task RemoveHolidayAsync(string resourceId, DateTime holiday);
    Task SetOvertimeAsync(string resourceId, DateTime date, double overtimeHours);
}
