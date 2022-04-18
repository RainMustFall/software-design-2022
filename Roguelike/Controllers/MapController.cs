using Roguelike.Core.Abstractions.Map;
using Roguelike.Map.Cells;

namespace Roguelike.Controllers;

using Map = Map.Map;

public class MapController
{
    public readonly Map Map;

    public MapController(Map map)
    {
        Map = map;
    }

    public bool Move(ICell toMove, int toX, int toY)
    {
        // TODO: move validation somewhere else
        if (toX >= Map.Cells.GetLength(0) || toX < 0 || toY >= Map.Cells.GetLength(1) || toY < 0 ||
            !Map.Cells[toX, toY].Empty())
            return false;
        var (fromX, fromY) = (toMove.X, toMove.Y);
        Map.Cells[fromX, fromY].RemoveCell(toMove);
        Map.Cells[toX, toY].PutCell(toMove);
        if (toMove is PlayableCell playableCell)
            playableCell.ParentCell = Map.Cells[toX, toY];
        return true;
    }
}