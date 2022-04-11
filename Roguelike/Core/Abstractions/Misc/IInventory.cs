using Roguelike.Core.Abstractions.Items;

namespace Roguelike.Core.Abstractions.Misc;

public interface IInventory : IEnumerable<IItem>
{
    bool TryPutItem(IItem item);
    // todo: how to remove items?
}