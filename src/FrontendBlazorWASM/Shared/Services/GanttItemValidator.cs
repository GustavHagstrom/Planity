using Microsoft.Extensions.Localization;
using Planity.FrontendBlazorWASM.Shared.Abstractions;
using Planity.FrontendBlazorWASM.Shared.Models;

namespace Planity.FrontendBlazorWASM.Shared.Services;

public class GanttItemValidator
{
    private readonly IStringLocalizer<GanttItemValidator> _localizer;
    private readonly IDateCalculator _dateCalculator;
    private readonly Func<string, Resource?> _resourceProvider;

    public GanttItemValidator(IStringLocalizer<GanttItemValidator> localizer, IDateCalculator dateCalculator, Func<string, Resource?> resourceProvider)
    {
        _localizer = localizer;
        _dateCalculator = dateCalculator;
        _resourceProvider = resourceProvider;
    }

    /// <summary>
    /// Validates dependencies for a GanttItem and returns warnings if dates are incorrect.
    /// </summary>
    public List<string> ValidateDependencies(IGanttItem item, List<Resource> resources, List<GanttItemLink> links)
    {
        var warnings = new List<string>();
        var itemResource = item is ProjectTask pt ? _resourceProvider(pt.ResourceId) : null;
        var itemEnd = _dateCalculator.CalculateEnd(item, resources);

        // Kontrollera Predecessors via länkar
        var predecessors = links.Where(l => l.To == item).Select(l => l.From);
        foreach (var predecessor in predecessors)
        {
            var predResource = predecessor is ProjectTask ptp ? _resourceProvider(ptp.ResourceId) : null;
            var predEnd = _dateCalculator.CalculateEnd(predecessor, resources);
            if (predEnd.HasValue && item.Start.HasValue && item.Start < predEnd)
            {
                warnings.Add(_localizer["Warning_PredecessorEndBeforeStart", item.Name, predecessor.Name]);
            }
        }

        // Kontrollera Successors via länkar
        var successors = links.Where(l => l.From == item).Select(l => l.To);
        foreach (var successor in successors)
        {
            var succResource = successor is ProjectTask pts ? _resourceProvider(pts.ResourceId) : null;
            var succStart = successor.Start;
            var itemEndVal = itemEnd;
            if (itemEndVal.HasValue && succStart.HasValue && itemEndVal > succStart)
            {
                warnings.Add(_localizer["Warning_SuccessorStartBeforeEnd", item.Name, successor.Name]);
            }
        }

        return warnings;
    }
}
