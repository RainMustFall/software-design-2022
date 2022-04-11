namespace Roguelike.Core.Abstractions.Map;

public interface ICell
{
    int X { get; }
    int Y { get; }
    char Render();
}