using Roguelike.Controllers.BaseControllers;
using Roguelike.Controllers.Misc;
using Roguelike.Map.Cells;
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
        if (MapController.Move(mob, newX, newY))
            (mob.Cell as MobCell)!.ParentCell = MapController.Map.Cells[newX, newY];
    }

    protected override void OnDeathInner() { }
}