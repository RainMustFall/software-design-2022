namespace Roguelike.Core.Abstractions.Map;

/// <summary>
/// Contains fields sufficient to describe a location of the object and an instruction of how to render it.
/// </summary>
public interface ICell
{
    int X { get; }
    int Y { get; }
    char Render();
}