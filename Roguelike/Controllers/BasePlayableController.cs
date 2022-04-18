using Roguelike.Controllers.Misc;
using Roguelike.Core.Abstractions.Controllers;

namespace Roguelike.Controllers;

public abstract class BasePlayableController : IPlayableController
{
    private readonly ControllerContainer controllerContainer;

    protected BasePlayableController(ControllerContainer controllerContainer)
    {
        this.controllerContainer = controllerContainer;
    }

    protected MapController MapController => controllerContainer.MapController;

    protected InventoryEquipmentController InventoryEquipmentController =>
        controllerContainer.InventoryEquipmentController;

    public abstract void Update();
}