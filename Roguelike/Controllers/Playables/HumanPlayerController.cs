using Roguelike.Controllers.BaseControllers;
using Roguelike.Controllers.Misc;
using Roguelike.Helpers;
using Roguelike.Map.Cells;
using Roguelike.Playables;
using SharpHook.Native;

namespace Roguelike.Controllers.Playables;

/// <summary>
/// Controller of player humanoid.
/// </summary>
public class HumanPlayerController : BasePlayableController
{
    private readonly ProgressibleHumanoid player;

    public HumanPlayerController(ControllerContainer controllerContainer,
        ProgressibleHumanoid player) : base(controllerContainer, player,
        BehaviourOptions.New().WithDeathHandling(player))
    {
        this.player = player;
    }

    protected override void UpdateInner()
    {
        var deltaX = 0;
        var deltaY = 0;

        if (ShortcutHandler.IsPressed(KeyCode.VcW))
            deltaY += -1;
        if (ShortcutHandler.IsPressed(KeyCode.VcS))
            deltaY += 1;
        if (ShortcutHandler.IsPressed(KeyCode.VcA))
            deltaX += -1;
        if (ShortcutHandler.IsPressed(KeyCode.VcD))
            deltaX += 1;

        if (deltaX != 0 || deltaY != 0)
        {
            var newX = player.Cell.X + deltaX;
            var newY = player.Cell.Y + deltaY;
            if (MapController.Move(player, newX, newY))
                (player.Cell as PlayableCell)!.ParentCell = MapController.Map.Cells[newX, newY];
        }
    }

    protected override void OnDeathInner()
    {
        Task.Run(async () =>
        {
            await Task.Delay(TimeSpan.FromSeconds(2));
            GameController.EndGame();
            Console.WriteLine("You died :(");
        });
    }
}