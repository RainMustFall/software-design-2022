using Roguelike.Core.Abstractions.Behaviours;

namespace Roguelike.Controllers;

public class InventoryEquipmentController
{
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