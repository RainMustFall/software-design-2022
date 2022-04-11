using Roguelike.Controllers.Misc;
using Roguelike.Playables;

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
        // todo: handle buttons and move player accordingly
        throw new NotImplementedException();
        // MapController.Move(player.Cell, 10, 12);
    }
}