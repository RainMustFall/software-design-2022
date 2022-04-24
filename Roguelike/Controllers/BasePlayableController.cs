using Roguelike.Controllers.Misc;
using Roguelike.Core.Abstractions.Behaviours;
using Roguelike.Core.Abstractions.Controllers;

namespace Roguelike.Controllers;

/// <summary>
/// Base class that contains convenient methods and fields that might be useful for many IPlayableController instances.
/// </summary>
public abstract class BasePlayableController : IPlayableController
{
    private readonly ControllerContainer controllerContainer;
    private readonly IPlayable playable;

    protected BasePlayableController(ControllerContainer controllerContainer, IPlayable playable)
    {
        this.controllerContainer = controllerContainer;
        this.playable = playable;
    }

    protected GameController GameController => controllerContainer.GameController;

    protected MapController MapController => controllerContainer.MapController;

    protected InventoryEquipmentController InventoryEquipmentController =>
        controllerContainer.InventoryEquipmentController;

    public abstract void Update();
    public void OnDeath()
    {
        GameController.OnPlayableDeath(this);
        if (playable is IRenderable renderable)
            MapController.RemoveCell(renderable.Cell);
        OnDeathInner();
    }

    protected abstract void OnDeathInner();
}