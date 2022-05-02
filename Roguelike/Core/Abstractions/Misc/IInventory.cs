using System.Collections.Generic;
using Roguelike.Core.Abstractions.Items;

namespace Roguelike.Core.Abstractions.Misc;

/// <summary>
/// Describes inventory and handles logic related to its capacity.
/// </summary>
public interface IInventory : IEnumerable<IItem>
{
    /// <summary>
    /// Try add inventory from inner collection.
    /// </summary>
    /// <param name="item">Inventory item to add</param>
    /// <returns>true if added</returns>
    bool TryPutItem(IItem item);
    /// <summary>
    /// Try remove inventory from inner collection.
    /// </summary>
    /// <param name="item">Inventory item to remove.</param>
    /// <returns>true if removed</returns>
    bool TryRemoveItem(IItem item);
    /// <summary>
    /// Get inventory by index in inner collection.
    /// </summary>
    /// <param name="index"></param>
    /// <returns>Inventory item instance or null, if incorrect index</returns>
    IItem? GetItemByIndex(int index);
}