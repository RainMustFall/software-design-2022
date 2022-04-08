using Roguelike.Core.Abstractions.Misc;

namespace Roguelike.Core.Abstractions.Behaviours;

public interface IHumanoid : IPlayable
{
    IInventory Inventory { get; }
    IEquipment Equipment { get; }
}