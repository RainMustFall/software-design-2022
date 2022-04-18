using Roguelike.Controllers.Misc;
using Roguelike.Helpers;
using Roguelike.Playables;
using SharpHook.Native;

namespace Roguelike.Controllers.Playables;

public class HumanPlayerController : BasePlayableController
{
    private readonly ProgressibleHumanoid player;

    public HumanPlayerController(ControllerContainer controllerContainer,
        ProgressibleHumanoid player) : base(controllerContainer)
    {
        this.player = player;
    }

    public override void Update()
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
            MapController.Move(player.Cell, player.Cell.X + deltaX, player.Cell.Y + deltaY);
    }
}