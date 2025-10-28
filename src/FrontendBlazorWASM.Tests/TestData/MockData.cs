using System;
using System.Collections.Generic;
using System.Linq;
using Planity.FrontendBlazorWASM.Shared.Models;

namespace Planity.FrontendBlazorWASM.Tests.TestData;

public static class MockData
{
 public static WorkCalendar CreateStandardCalendar(Action<WorkCalendar>? configure = null)
 {
 var cal = new WorkCalendar();
 configure?.Invoke(cal);
 return cal;
 }

 public static List<Resource> GetDefaultResources()
 {
 return new List<Resource>
 {
 new Resource
 {
 Id = "res-1",
 OrganizationId = "org-1",
 Name = "Team Alpha",
 Workers =1,
 Efficiency =1.0,
 WorkCalendar = CreateStandardCalendar()
 },
 new Resource
 {
 Id = "res-2",
 OrganizationId = "org-1",
 Name = "Team Beta",
 Workers =2,
 Efficiency =1.0,
 WorkCalendar = CreateStandardCalendar(cal =>
 {
 // Example holiday
 cal.Holidays.Add(new Holyday { Date = new DateOnly(2025,12,24), Name = "Julafton" });
 })
 }
 };
 }

 public static List<ProjectTask> GetDefaultTasks(IEnumerable<Resource> resources)
 {
 var res1 = resources.First(r => r.Id == "res-1");
 var res2 = resources.First(r => r.Id == "res-2");
 return new List<ProjectTask>
 {
 new ProjectTask
 {
 Id = "task-1",
 OrganizationId = "org-1",
 Name = "Planera",
 Start = new DateTime(2025,03,03,8,0,0),
 WorkHours =8,
 ResourceId = res1.Id
 },
 new ProjectTask
 {
 Id = "task-2",
 OrganizationId = "org-1",
 Name = "Genomföra",
 Start = new DateTime(2025,03,04,8,0,0),
 WorkHours =12,
 ResourceId = res2.Id
 }
 };
 }
}
