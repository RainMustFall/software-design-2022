using Roguelike.Controllers.Misc;
using Roguelike.Controllers.Playables;
using Roguelike.Core.Abstractions.Controllers;
using Roguelike.Map.Cells;
using Roguelike.Playables;

namespace Roguelike.Controllers;

public class GameController
{
    public readonly List<IPlayableController> PlayableControllers = new();
    public MapController MapController { get; }

    public GameController(MapController mapController)
    {
        MapController = mapController;
    }

    // todo: other factories and game methods...
    public ProgressibleHumanoid CreateHumanPlayer(CompositeCell initialPosition)
    {
        var cell = new PlayableCell(initialPosition);
        initialPosition.PutCell(cell);
        var humanoid = new ProgressibleHumanoid(cell);
        cell.Renderable = humanoid;
        var humanPlayerController = new HumanPlayerController(GetControllerContainer(), humanoid);
        PlayableControllers.Add(humanPlayerController);
        return humanoid;
    }

    private ControllerContainer GetControllerContainer()
    {
        return new ControllerContainer(MapController);
    }
}