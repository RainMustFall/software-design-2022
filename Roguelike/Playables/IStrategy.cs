using Roguelike.Core.Abstractions.Map;

namespace Roguelike.Mobs;

/// <summary>
/// Controls movement strategy of a playable agent.
/// It's main purpose is to be used in conjunction with <see cref="BaseMob"/>.
/// </summary>
public interface IStrategy
{
    public (int, int) NextCoordinates(ICell cell);
}