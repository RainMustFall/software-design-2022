using Roguelike.Controllers.Misc;
using Roguelike.Core.Abstractions.Behaviours;
using Roguelike.Core.Abstractions.Controllers;
using Roguelike.Map.Cells;
using Roguelike.Mobs;

namespace Roguelike.Controllers;

/// <summary>
/// Base class that contains convenient methods and fields that might be useful for many IPlayableController instances.
/// </summary>
public class MobController : BasePlayableController
{
    private readonly BaseMob mob;

    public MobController(ControllerContainer controllerContainer, BaseMob mob)
        : base(controllerContainer, mob)
    {
        this.mob = mob;
    }

    public override void Update()
    {
        (int newX, int newY) = mob.MovementStrategy.NextCoordinates(mob.Cell);
        if (MapController.Move(mob, newX, newY))
            (mob.Cell as MobCell)!.ParentCell = MapController.Map.Cells[newX, newY];
    }

    protected override void OnDeathInner()
    {
    }
}