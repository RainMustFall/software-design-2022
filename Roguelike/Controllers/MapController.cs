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
        var (fromX, fromY) = (toMove.X, toMove.Y);
        // todo: validate whether can move 
        Map.Cells[fromX, fromY].RemoveCell(toMove);
        Map.Cells[toX, toY].PutCell(toMove);
        if (toMove is PlayableCell playableCell)
            playableCell.ParentCell = Map.Cells[toX, toY];
        return true;
    }
}