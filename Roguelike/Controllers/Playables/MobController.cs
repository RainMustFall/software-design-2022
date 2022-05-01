using Roguelike.Controllers.BaseControllers;
using Roguelike.Controllers.Misc;
using Roguelike.Core.Abstractions.Map;
using Roguelike.Helpers;
using Roguelike.Map.Cells;
using Roguelike.Playables;
using Roguelike.Mobs;

namespace Roguelike.Controllers.Playables;

/// <summary>
/// Base class that contains convenient methods and fields that might be useful for many IPlayableController instances.
/// </summary>
public class MobController : BasePlayableController
{
    private readonly BaseMob mob;

    public MobController(ControllerContainer controllerContainer, BaseMob mob)
        : base(controllerContainer, mob, BehaviourOptions.New().WithDeathHandling(mob).WithConfusionHandling(mob))
    {
        this.mob = mob;
    }

    protected override void UpdateInner()
    {
        var (newX, newY) = mob.MovementStrategy.NextCoordinates(mob.Cell);
        var newMobCell = MapController.Map.Cells[newX, newY];
        if (newMobCell.ContainsPlayer())
            OnTriggerRenderingCreature(newMobCell);
        if (MapController.Move(mob.Cell, newX, newY))
            (mob.Cell as MobCell)!.ParentCell = MapController.Map.Cells[newX, newY];
    }

    public override void OnTriggerRenderingCreature(ICell cell)
    {
        if (cell is CompositeCell compositeCell)
        {
            BattleController.Battle(mob, compositeCell.GetCreatureInCell());
        }
    }

    protected override void OnDeathInner() { }
}