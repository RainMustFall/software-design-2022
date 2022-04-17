using Roguelike.Core.Abstractions.Behaviours;
using Roguelike.Core.Abstractions.Map;
using Roguelike.Core.Abstractions.Misc;
using Roguelike.Properties;
using Roguelike.Equipments;
using Roguelike.Inventories;

namespace Roguelike.Playables;

public class ProgressibleHumanoid : IProgressible, IHumanoid, IRenderable
{
    private CreatureProperties BaseProperties = new CreatureProperties(100, 10);

    public ProgressibleHumanoid(ICell humanoidCell)
    {
        Cell = humanoidCell;
    }

    public ProgressionProperties Progression { get; set; } = new ProgressionProperties(1, 0);
    public IInventory Inventory { get; } = new SimpleInventory();
    public IEquipment Equipment { get; } = new SimpleEquipment();
    public CreatureState State { get; } = new CreatureState(30);
    public CreatureProperties Properties => GetCurrentProperties();
    public ICell Cell { get; }

    private CreatureProperties GetCurrentProperties()
    {
        var clone = BaseProperties;
        // todo: apply equipment bonuses to clone
        return clone;
    }
}