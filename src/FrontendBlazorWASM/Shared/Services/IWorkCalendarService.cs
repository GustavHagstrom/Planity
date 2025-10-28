using Planity.FrontendBlazorWASM.Shared.Models;

namespace Planity.FrontendBlazorWASM.Shared.Services;

public interface IWorkCalendarService
{
    Task<WorkCalendar> GetWorkCalendarAsync(string resourceId);
    Task SetWorkPeriodsByDayOfWeekAsync(string resourceId, DayOfWeek day, List<(TimeSpan Start, TimeSpan End, double BreakDuration)> periods);
    Task AddHolidayAsync(string resourceId, DateTime holiday);
    Task RemoveHolidayAsync(string resourceId, DateTime holiday);
    Task SetOvertimeAsync(string resourceId, DateTime date, double overtimeHours);
}
