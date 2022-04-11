using Roguelike.Core.Abstractions.Controllers;

namespace Roguelike.Controllers;

public abstract class BasePlayableController : IPlayableController
{
    protected BasePlayableController(MapController mapController)
    {
        MapController = mapController;
    }

    protected MapController MapController;

    public abstract void Update();
}