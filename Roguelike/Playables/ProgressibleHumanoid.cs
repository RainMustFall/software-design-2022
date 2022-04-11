using Roguelike.Core.Abstractions.Behaviours;
using Roguelike.Core.Abstractions.Misc;
using Roguelike.Core.Models;
using Roguelike.Equipments;
using Roguelike.Inventories;

namespace Roguelike.Playables;

public class ProgressibleHumanoid : IProgressible, IHumanoid
{
    public ProgressionProperties Progression { get; set; } = new(1, 0);
    public IInventory Inventory { get; } = new SimpleInventory();
    public IEquipment Equipment { get; } = new SimpleEquipment();
    public CreatureState State { get; } = new(30); // todo: to constructor
    public CreatureProperties Properties { get; } = new(100, 10);
}