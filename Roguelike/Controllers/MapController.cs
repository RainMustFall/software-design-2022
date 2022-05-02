using System.Data;
using System.Net.Mail;
using Roguelike.Core.Abstractions.Map;
using Roguelike.Core.Abstractions.Behaviours;
using Roguelike.Map.Cells;

namespace Roguelike.Controllers;

using Map = Map.Map;

/// <summary>
/// Controls interactions of playable controllers with map (basically, movement).
/// </summary>
public class MapController
{
    public readonly Map Map;

    public MapController(Map map)
    {
        Map = map;
    }

    public bool Move(ICell toMove, int toX, int toY)
    {
        var cell = GetCell(toX, toY);
        if (cell == null || !cell.Empty())
            return false;
        var (fromX, fromY) = (toMove.X, toMove.Y);
        Map.Cells[fromX, fromY].RemoveCell(toMove);
        Map.Cells[toX, toY].PutCell(toMove);
        return true;
    }

    public void RemoveCell(ICell cell)
    {
        Map.Cells[cell.X, cell.Y].RemoveCell(cell);
    }

    public CompositeCell? GetCell(int x, int y)
    {
        if (x >= Map.Cells.GetLength(0) || x < 0 || y >= Map.Cells.GetLength(1) || y < 0)
            return null;
        return Map.Cells[x, y];
    }
}