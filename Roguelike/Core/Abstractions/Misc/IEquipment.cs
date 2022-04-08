using Roguelike.Core.Abstractions.Items;

namespace Roguelike.Core.Abstractions.Misc;

public interface IEquipment
{
    IItem? Helmet { get; }
    IItem? Body { get; }
    IItem? Weapon { get; }

    IItem? TakeHelmetOff();
    IItem? TakeBodyOff();
    IItem? TakeWeaponOff();
}