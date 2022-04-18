using Roguelike.Core.Abstractions.Behaviours;

namespace Roguelike.Core.Abstractions.Items;

/// <summary>
/// Allows playable instance to interact with item (i.e., to apply some buffs).
/// </summary>
public interface IInteractableItem : IItem
{
    void Interact(IPlayable playable);
}