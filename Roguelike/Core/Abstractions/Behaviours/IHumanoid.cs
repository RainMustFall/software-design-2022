using Roguelike.Core.Abstractions.Misc;

namespace Roguelike.Core.Abstractions.Behaviours;

/// <summary>
/// Contains fields that are applicable to every Humanoid, such as Inventory and Equipment.
/// </summary>
public interface IHumanoid : ICreature
{
    IInventory Inventory { get; }
    IEquipment Equipment { get; }
}