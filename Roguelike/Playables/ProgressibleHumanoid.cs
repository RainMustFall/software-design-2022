using Roguelike.Core.Abstractions.Behaviours;
using Roguelike.Core.Abstractions.Map;
using Roguelike.Core.Abstractions.Misc;
using Roguelike.Core.Models;
using Roguelike.Equipments;
using Roguelike.Inventories;

namespace Roguelike.Playables;

public class ProgressibleHumanoid : IProgressible, IHumanoid, IRenderable
{
    private CreatureProperties BaseProperties = new(100, 10); // todo: to constructor

    public ProgressibleHumanoid(ICell humanoidCell)
    {
        Cell = humanoidCell;
    }

    public ProgressionProperties Progression { get; set; } = new(1, 0);
    public IInventory Inventory { get; } = new SimpleInventory();
    public IEquipment Equipment { get; } = new SimpleEquipment();
    public CreatureState State { get; } = new(30); // todo: to constructor
    public CreatureProperties Properties => GetCurrentProperties();
    public ICell Cell { get; }

    private CreatureProperties GetCurrentProperties()
    {
        var clone = BaseProperties with { };
        // todo: apply equipment bonuses to clone
        return clone;
    }
}