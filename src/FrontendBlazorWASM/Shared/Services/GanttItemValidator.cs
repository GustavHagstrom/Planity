using Microsoft.Extensions.Localization;
using Planity.FrontendBlazorWASM.Shared.Abstractions;

namespace Planity.FrontendBlazorWASM.Shared.Services;

public class GanttItemValidator
{
    private readonly IStringLocalizer<GanttItemValidator> _localizer;

    public GanttItemValidator(IStringLocalizer<GanttItemValidator> localizer)
    {
        _localizer = localizer;
    }

    /// <summary>
    /// Validates dependencies for a GanttItem and returns warnings if dates are incorrect.
    /// </summary>
    public List<string> ValidateDependencies(IGanttItem item)
    {
        var warnings = new List<string>();

        // Kontrollera Predecessors
        if (item.Predecessors != null)
        {
            foreach (var predecessor in item.Predecessors)
            {
                if (predecessor.End.HasValue && item.Start.HasValue && item.Start < predecessor.End)
                {
                    warnings.Add($"Startdatum för '{item.Name}' är före slutdatumet för föregående beroende '{predecessor.Name}'.");
                }
            }
        }

        // Kontrollera Successors
        if (item.Successors != null)
        {
            foreach (var successor in item.Successors)
            {
                if (item.End.HasValue && successor.Start.HasValue && item.End > successor.Start)
                {
                    warnings.Add($"Slutdatum för '{item.Name}' är efter startdatumet för efterföljande beroende '{successor.Name}'.");
                }
            }
        }

        return warnings;
    }
}
