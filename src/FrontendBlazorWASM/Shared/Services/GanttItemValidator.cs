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
    public List<string> ValidateDependencies(IGanttItem item, List<Resource> resources)
    {
        var warnings = new List<string>();
        var itemResource = item is ProjectTask pt ? _resourceProvider(pt.ResourceId) : null;
        var itemEnd = _dateCalculator.CalculateEnd(item, resources);

        // Kontrollera Predecessors
        if (item.Predecessors != null)
        {
            foreach (var predecessor in item.Predecessors)
            {
                var predResource = predecessor is ProjectTask ptp ? _resourceProvider(ptp.ResourceId) : null;
                var predEnd = _dateCalculator.CalculateEnd(predecessor, resources);
                if (predEnd.HasValue && item.Start.HasValue && item.Start < predEnd)
                {
                    warnings.Add(_localizer["Warning_PredecessorEndBeforeStart", item.Name, predecessor.Name]);
                }
            }
        }

        // Kontrollera Successors
        if (item.Successors != null)
        {
            foreach (var successor in item.Successors)
            {
                var succResource = successor is ProjectTask pts ? _resourceProvider(pts.ResourceId) : null;
                var succStart = successor.Start;
                var itemEndVal = itemEnd;
                if (itemEndVal.HasValue && succStart.HasValue && itemEndVal > succStart)
                {
                    warnings.Add(_localizer["Warning_SuccessorStartBeforeEnd", item.Name, successor.Name]);
                }
            }
        }

        return warnings;
    }
}
