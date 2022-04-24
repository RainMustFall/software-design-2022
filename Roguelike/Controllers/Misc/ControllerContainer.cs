namespace Roguelike.Controllers.Misc;

/// <summary>
/// Small container that contains important controllers.
/// It's only purpose is to improve readability
/// </summary>
public record ControllerContainer(GameController GameController, MapController MapController, InventoryEquipmentController InventoryEquipmentController);