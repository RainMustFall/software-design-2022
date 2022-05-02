using Roguelike.Core.Abstractions.Behaviours;
using Roguelike.Core.Abstractions.Map;
using Roguelike.Core.Abstractions.Misc;
using Roguelike.Properties;
using Roguelike.Equipments;
using Roguelike.Inventories;
using Roguelike.Map.Cells;

namespace Roguelike.Playables;

/// <summary>
/// A model of humanoid which is also <see cref="IProgressible"/>.
/// </summary>
public class ProgressibleHumanoid : IProgressible, IHumanoid
{
    private readonly CreatureProperties baseProperties = new(100, 34);

    public ProgressibleHumanoid(CompositeCell initialPosition)
    {
        var playableCell = new PlayableCell(initialPosition, this);
        Cell = playableCell;
        Progression.MoveToNextLevel += UpdateHealthProperties;
    }

    public IInventory Inventory { get; } = new SimpleInventory();
    public IEquipment Equipment { get; } = new SimpleEquipment();
    public CreatureState State { get; } = new(100);
    public CreatureProperties Properties => GetCurrentProperties();
    public ICell Cell { get; }

    public ProgressionProperties Progression { get; set; } = new(1, 0);

    private CreatureProperties GetCurrentProperties()
    {
        var clone = baseProperties;
        // todo: apply equipment bonuses to clone
        return clone;
    }

    private void UpdateHealthProperties(object sender, MoveToNextLevelArgs e)
    {
        var newMaxHealth = e.Level * 100;
        Progression.RefreshExperience();
        baseProperties.SetMaxHealth(newMaxHealth);
        State.UpdateHealth(newMaxHealth);
    }
}