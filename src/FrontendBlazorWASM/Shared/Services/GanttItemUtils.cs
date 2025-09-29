using Planity.FrontendBlazorWASM.Shared.Abstractions;

namespace Planity.FrontendBlazorWASM.Shared.Services;

public static class GanttItemUtils
{
    public static IEnumerable<IGanttItem> ItemsWithExpand(IEnumerable<IGanttItem>? items)
    {
        foreach (var item in items ?? [])
        {
            yield return item;
            if (item.IsExpanded && item.Children is not null)
            {
                foreach (var child in ItemsWithExpand(item.Children))
                    yield return child;
            }
        }
    }

    public static IEnumerable<(IGanttItem item, int level)> ItemsWithExpandLevel(IEnumerable<IGanttItem>? items, int level = 0)
    {
        foreach (var item in items ?? [])
        {
            yield return (item, level);
            if (item.IsExpanded && item.Children is not null)
            {
                foreach (var child in ItemsWithExpandLevel(item.Children, level + 1))
                    yield return child;
            }
        }
    }
}
