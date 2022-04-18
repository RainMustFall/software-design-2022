using Roguelike.Core.Abstractions.Items;

namespace Roguelike.Core.Abstractions.Misc;

/// <summary>
/// Contains fields and methods sufficient to manipulate with creature equipment.
/// </summary>
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
    /// <summary>
    /// Puts a body on.
    /// </summary>
    /// <param name="body">Body to wear</param>
    /// <returns>Previous body if it was on</returns>
    IItem? PutBodyOn(IItem body);
    /// <summary>
    /// Puts a weapon on.
    /// </summary>
    /// <param name="weapon">Weapon to wear</param>
    /// <returns>Previous weapon if it was on</returns>
    IItem? PutWeaponOn(IItem weapon);

    IItem? UnwearHelmet();
    IItem? UnwearBody();
    IItem? UnwearWeapon();
}