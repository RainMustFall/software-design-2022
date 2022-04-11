using Roguelike.Core.Abstractions.Misc;

namespace Roguelike.Core.Abstractions.Behaviours;

public interface IHumanoid : ICreature
{
    IInventory Inventory { get; }
    IEquipment Equipment { get; }
}