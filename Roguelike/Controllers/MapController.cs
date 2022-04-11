using Roguelike.Core.Abstractions.Map;
using Roguelike.Map.Cells;

namespace Roguelike.Controllers;

using Map = Map.Map;

public class MapController
{
    private readonly Map map;

    public MapController(Map map)
    {
        this.map = map;
    }

    public bool Move(ICell toMove, int toX, int toY)
    {
        var (fromX, fromY) = (toMove.X, toMove.Y);
        // todo: validate whether can move 
        map.Cells[fromX, fromY].RemoveCell(toMove);
        map.Cells[toX, toY].PutCell(toMove);
        if (toMove is PlayableCell playableCell)
            playableCell.ParentCell = map.Cells[toX, toY];
        return true;
    }
}