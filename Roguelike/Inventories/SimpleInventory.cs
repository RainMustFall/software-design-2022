using System.Collections;
using Roguelike.Core.Abstractions.Items;
using Roguelike.Core.Abstractions.Misc;

namespace Roguelike.Inventories;

public class SimpleInventory : IInventory
{
    private readonly List<IItem> items = new();

    public IEnumerator<IItem> GetEnumerator()
    {
        return items.GetEnumerator();
    }

    public bool TryPutItem(IItem item)
    {
        items.Add(item);
        return true;
    }
    public bool TryRemoveItem(IItem toRemove)
    {
        return items.Remove(toRemove);
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}