using Planity.FrontendBlazorWASM.Shared.Abstractions;

namespace Planity.FrontendBlazorWASM.Shared.State;

public class GanttViewStateContainer
{
    private HashSet<IGanttItem> _items = new();

    public IReadOnlyCollection<IGanttItem> Items => _items;

    public event Action? OnChange;

    public void SetItems(IEnumerable<IGanttItem> items)
    {
        _items = items.ToHashSet();
        NotifyStateChanged();
    }

    public void AddItem(IGanttItem item)
    {
        _items.Add(item);
        NotifyStateChanged();
    }

    public void RemoveItem(string id)
    {
        _items.RemoveWhere(i => i.Id == id);
        NotifyStateChanged();
    }

    private void NotifyStateChanged() => OnChange?.Invoke();
}
