using Roguelike.Controllers.BaseControllers;
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
    private readonly IPlayable? playable;
    private readonly BehaviourOptions options;
    private bool dead;

    protected BasePlayableController(ControllerContainer controllerContainer,
        IPlayable? playable,
        BehaviourOptions? options = null)
    {
        this.controllerContainer = controllerContainer;
        this.playable = playable;
        this.options = options ?? BehaviourOptions.New();
    }

    protected GameController GameController => controllerContainer.GameController;

    protected MapController MapController => controllerContainer.MapController;

    protected InventoryEquipmentController InventoryEquipmentController =>
        controllerContainer.InventoryEquipmentController;

    public void Update()
    {
        if (dead)
            return;

        foreach (var action in options.OnUpdateActions())
            action(this);
        UpdateInner();
    }

    protected abstract void UpdateInner();
    public void OnDeath()
    {
        GameController.OnPlayableDeath(this);
        if (playable is IRenderable renderable)
            MapController.RemoveCell(renderable.Cell);
        OnDeathInner();
        dead = true;
    }

    protected abstract void OnDeathInner();
}