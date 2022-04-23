using Roguelike.Core.Abstractions.Items;

namespace Roguelike.Core.Abstractions.Misc;

/// <summary>
/// Describes inventory and handles logic related to its capacity.
/// </summary>
public interface IInventory : IEnumerable<IItem>
{
    bool TryPutItem(IItem item);
    bool TryRemoveItem(IItem item);
    IItem? GetItemByIndex(int index);
}