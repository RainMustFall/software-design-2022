using Roguelike.Core.Abstractions.Map;

namespace Roguelike.Mobs;

public interface IStrategy
{
    public (int, int) NextCoordinates(ICell cell);
}