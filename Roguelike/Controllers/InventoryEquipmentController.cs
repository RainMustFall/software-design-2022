using Roguelike.Core.Abstractions.Behaviours;
using Roguelike.Core.Abstractions.Items;

namespace Roguelike.Controllers;

/// <summary>
/// Controls interactions of humanoid instances with equipment and inventory. 
/// </summary>
public class InventoryEquipmentController
{
    public void PutHelmetOn(IHumanoid humanoid, IItem helmet)
    {
        var existing = humanoid.Equipment.PutHelmetOn(helmet);
        if (existing != null)
            humanoid.Inventory.TryPutItem(existing);
        humanoid.Inventory.TryRemoveItem(helmet);
    }
    public void PutBodyOn(IHumanoid humanoid, IItem body)
    {
        var existing = humanoid.Equipment.PutBodyOn(body);
        if (existing != null)
            humanoid.Inventory.TryPutItem(existing);
        humanoid.Inventory.TryRemoveItem(body);
    }

    public void PutWeaponOn(IHumanoid humanoid, IItem weapon)
    {
        var existing = humanoid.Equipment.PutWeaponOn(weapon);
        if (existing != null)
            humanoid.Inventory.TryPutItem(existing);
        humanoid.Inventory.TryRemoveItem(weapon);
    }

    public void UnwearHelmet(IHumanoid humanoid)
    {
        var helmet = humanoid.Equipment.UnwearHelmet();
        if (helmet != null)
            humanoid.Inventory.TryPutItem(helmet);
    }

    public void UnwearBody(IHumanoid humanoid)
    {
        var body = humanoid.Equipment.UnwearBody();
        if (body != null)
            humanoid.Inventory.TryPutItem(body);
    }

    public void UnwearWeapon(IHumanoid humanoid)
    {
        var weapon = humanoid.Equipment.UnwearWeapon();
        if (weapon != null)
            humanoid.Inventory.TryPutItem(weapon);
    }
}