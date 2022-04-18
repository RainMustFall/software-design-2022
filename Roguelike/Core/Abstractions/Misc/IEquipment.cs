using Roguelike.Core.Abstractions.Items;

namespace Roguelike.Core.Abstractions.Misc;

public interface IEquipment
{
    IItem? Helmet { get; }
    IItem? Body { get; }
    IItem? Weapon { get; }

    /// <summary>
    /// Puts a helmet on.
    /// </summary>
    /// <param name="helmet">Helmet to wear</param>
    /// <returns>Previous helmet if it was on</returns>
    IItem? PutHelmetOn(IItem helmet);

    IItem? PutBodyOn(IItem body);
    IItem? PutWeaponOn(IItem weapon);

    IItem? UnwearHelmet();
    IItem? UnwearBody();
    IItem? UnwearWeapon();
}