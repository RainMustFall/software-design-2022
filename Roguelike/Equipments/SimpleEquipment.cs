using Roguelike.Core.Abstractions.Items;
using Roguelike.Core.Abstractions.Misc;

namespace Roguelike.Equipments;

public class SimpleEquipment : IEquipment
{
    public IItem? Helmet { get; private set; }
    public IItem? Body { get; private set; }
    public IItem? Weapon { get; private set; }

    public IItem? PutHelmetOn(IItem helmet)
    {
        var previous = Helmet;
        Helmet = helmet;
        return previous;
    }

    public IItem? PutBodyOn(IItem body)
    {
        var previous = Body;
        Body = body;
        return previous;
    }

    public IItem? PutWeaponOn(IItem weapon)
    {
        var previous = Weapon;
        Weapon = weapon;
        return previous;
    }
}