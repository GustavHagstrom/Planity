using NUnit.Framework;
using Planity.FrontendBlazorWASM.Shared.Models;
using Planity.FrontendBlazorWASM.Shared.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Planity.FrontendBlazorWASM.Tests.TestData;
using Planity.FrontendBlazorWASM.Tests.TestDoubles;

namespace Planity.FrontendBlazorWASM.Tests;

[TestFixture]
public class DateCalculatorTests
{
    private static Resource CreateResource(string id = "res-1", int workers = 1, double efficiency = 1.0, Action<WorkCalendar>? configure = null)
    {
        var calendar = new WorkCalendar(); // uses default periods (Mon-Thu08:00-16:30, Fri08:00-14:00,1h break)
        configure?.Invoke(calendar);
        return new Resource
        {
            Id = id,
            Name = "Test Resource",
            Workers = workers,
            Efficiency = efficiency,
            WorkCalendar = calendar
        };
    }

    private static DateCalculator CreateCalculator(IResourceService? svc = null)
        => new(resourceService: svc ?? new FakeResourceService(MockData.GetDefaultResources(), MockData.GetDefaultTasks(MockData.GetDefaultResources())));

    [Test]
    public void CalculateEnd_Task8Hours_SpansToTuesdayMorning()
    {
        // Arrange
        var resource = CreateResource();
        var resources = new List<Resource> { resource };
        var start = new DateTime(2025, 3, 3, 8, 0, 0); // Monday
        var task = new ProjectTask { Start = start, WorkHours = 8, ResourceId = resource.Id };
        var sut = CreateCalculator();

        // Act
        var end = sut.CalculateEnd(task, resources);

        // Assert
        // With7.5h effective work on Monday,0.5h remains -> finishes Tuesday08:30
        var expected = new DateTime(2025, 3, 4, 8, 30, 0);
        Assert.That(end, Is.EqualTo(expected));
    }

    [Test]
    public void CalculateEnd_RespectsWorkersAndEfficiency_FinishesEarlier()
    {
        // Arrange
        var resource = CreateResource(workers: 2, efficiency: 1.0);
        var resources = new List<Resource> { resource };
        var start = new DateTime(2025, 3, 3, 8, 0, 0); // Monday
        var task = new ProjectTask { Start = start, WorkHours = 8, ResourceId = resource.Id };
        var sut = CreateCalculator();

        // Act
        var end = sut.CalculateEnd(task, resources);

        // Assert
        // workerEff =2 =>8h work becomes4h elapsed => ends12:00 Monday
        var expected = new DateTime(2025, 3, 3, 12, 0, 0);
        Assert.That(end, Is.EqualTo(expected));
    }

    [Test]
    public void CalculateEnd_SkipsHoliday_GoesToNextWorkDay()
    {
        // Arrange
        var resource = CreateResource(configure: cal =>
        {
            cal.Holidays.Add(new Holyday { Date = new DateOnly(2025, 3, 3), Name = "Holiday Monday" });
        });
        var resources = new List<Resource> { resource };
        var start = new DateTime(2025, 3, 3, 8, 0, 0); // Monday but holiday
        var task = new ProjectTask { Start = start, WorkHours = 4, ResourceId = resource.Id };
        var sut = CreateCalculator();

        // Act
        var end = sut.CalculateEnd(task, resources);

        // Assert
        var expected = new DateTime(2025, 3, 4, 12, 0, 0); // Tuesday12:00
        Assert.That(end, Is.EqualTo(expected));
    }

    [Test]
    public void CalculateEnd_OvertimeOnWeekend_AllowsCompletion()
    {
        // Arrange
        var resource = CreateResource(configure: cal =>
        {
            // Add 3 hours overtime on Saturday
            cal.Overtime.Add(new Overtime { Date = new DateOnly(2025, 3, 8), From = TimeSpan.FromHours(17), To = TimeSpan.FromHours(20) });
        });
        var resources = new List<Resource> { resource };
        var start = new DateTime(2025, 3, 8, 8, 0, 0); // Saturday
        var task = new ProjectTask { Start = start, WorkHours = 2, ResourceId = resource.Id };
        var sut = CreateCalculator();

        // Act
        var end = sut.CalculateEnd(task, resources);

        // Assert
        // No ordinary work on Saturday, overtime starts08:00 => finishes10:00
        var expected = new DateTime(2025, 3, 8, 10, 0, 0);
        Assert.That(end, Is.EqualTo(expected));
    }

    [Test]
    public void CalculateStart_SundayMovesToMondayFirstPeriod()
    {
        // Arrange
        var resource = CreateResource();
        var resources = new List<Resource> { resource };
        var start = new DateTime(2025, 3, 2, 10, 0, 0); // Sunday
        var task = new ProjectTask { Start = start, ResourceId = resource.Id };
        var sut = CreateCalculator();

        // Act
        var adjusted = sut.CalculateStart(task, resources);

        // Assert
        var expected = new DateTime(2025, 3, 3, 8, 0, 0); // Monday08:00
        Assert.That(adjusted, Is.EqualTo(expected));
    }

    [Test]
    public async Task CalculateEndAsync_UsesResourceService()
    {
        // Arrange
        var resources = MockData.GetDefaultResources();
        var tasks = MockData.GetDefaultTasks(resources);
        var fakeSvc = new FakeResourceService(resources, tasks);
        var sut = CreateCalculator(fakeSvc);
        var task = tasks[0];

        // Act
        var end = await sut.CalculateEndAsync(task);

        // Assert
        Assert.That(end, Is.Not.Null);
    }

    [Test]
    public void IsWorkDay_DefaultCalendar_MondayTrue_SundayFalse()
    {
        // Arrange
        var calendar = new WorkCalendar();
        var sut = CreateCalculator();
        var monday = new DateTime(2025, 3, 3);
        var sunday = new DateTime(2025, 3, 2);

        Assert.Multiple(() =>
        {
            // Act + Assert
            Assert.That(sut.IsWorkDay(calendar, monday), Is.True);
            Assert.That(sut.IsWorkDay(calendar, sunday), Is.False);
        });
    }
}
