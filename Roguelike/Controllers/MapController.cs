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
        if (toX >= Map.Cells.GetLength(0) || toX < 0 || toY >= Map.Cells.GetLength(1) || toY < 0)
            return false;
        if (!Map.Cells[toX, toY].Empty())
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
}