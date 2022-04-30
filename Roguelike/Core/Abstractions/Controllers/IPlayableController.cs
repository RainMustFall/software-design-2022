using Roguelike.Core.Abstractions.Map;

namespace Roguelike.Core.Abstractions.Controllers;

/// <summary>
/// Contains fields that are applicable to every playable actor of the game.
/// I.e. methods that are required for successful initialization or disposal (in case of death of playable instance).
/// </summary>
public interface IPlayableController
{
    void Update();
    void OnDeath();
    void OnTrigger(ICell cell);
}