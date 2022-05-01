using System;
using System.Diagnostics;
using Roguelike.Core.Abstractions.Items;
using Roguelike.Map.Cells;

namespace Roguelike.Map;

/// <summary>
///  The class that stores the current state of the map
/// </summary>
public class Map
{
    public Map(int width, int height, int inventoryCount, bool generate = true)
    {
        Cells = new CompositeCell[width, height];
        for (var x = 0; x < width; ++x)
        for (var y = 0; y < height; ++y)
            Cells[x, y] = new CompositeCell(x, y);
        if (generate)
        {
            GenerateMap();
            GenerateInventory(inventoryCount);
        }
    }

    public CompositeCell[,] Cells { get; }

    private void GenerateMap(double placementThreshold = 0.6)
    {
        var random = new Random();
        var rMax = Cells.GetUpperBound(0);
        var cMax = Cells.GetUpperBound(1);

        for (var i = 0; i <= rMax; i++)
        for (var j = 0; j <= cMax; j++)
            if (i == 0 || j == 0 || i == rMax || j == cMax)
            {
                Cells[i, j].PutCell(new WallCell(i, j));
            }
            else if (i % 2 == 0 && j % 2 == 0)
            {
                var nextDouble = random.NextDouble();
                if (nextDouble > placementThreshold)
                {
                    Cells[i, j].PutCell(new WallCell(i, j));
                    var a = nextDouble < .5 ? 0 : nextDouble < .5 ? -1 : 1;
                    var b = a != 0 ? 0 : nextDouble < .5 ? -1 : 1;
                    Cells[i + a, j + b].PutCell(new WallCell(i, j));
                }
            }
    }

    private void GenerateInventory(int inventoryCount)
    {
        var random = new Random();
        var rMax = Cells.GetUpperBound(0);
        var cMax = Cells.GetUpperBound(1);
        for (var i = 0; i < inventoryCount;)
        {
            int rowRandom = random.Next(1, rMax - 1);
            int columnRandom = random.Next(1, cMax - 1);
            if (Cells[rowRandom, columnRandom].Empty())
            {
                if (i % 2 == 0)
                    Cells[rowRandom, columnRandom].PutCell(new ItemCell(rowRandom, columnRandom, ItemType.Weapon));
                else if (i % 3 == 0)
                    Cells[rowRandom, columnRandom].PutCell(new ItemCell(rowRandom, columnRandom, ItemType.Helmet));
                else
                    Cells[rowRandom, columnRandom].PutCell(new ItemCell(rowRandom, columnRandom, ItemType.Body));
                i++;
            }
        }
    }
    
    /// <summary>
    /// Returns the topmost left cell, which is not
    /// occupied by anything, and null if there is no such cell
    /// </summary>
    public CompositeCell? GetFirstEmptyCell()
    {
        for (var i = 0; i <= Cells.GetUpperBound(0); i++)
        for (var j = 0; j <= Cells.GetUpperBound(1); j++)
            if (Cells[i, j].Empty())
                return Cells[i, j];
        return null;
    }

    /// <summary>
    /// Random cell, which is not
    /// occupied by anything, and null if there is no such cell
    /// </summary>
    public CompositeCell? GetRandomEmptyCell()
    {
        // prevent endless loop
        if (GetFirstEmptyCell() == null)
            return null;

        var r = new Random();
        while (true)
        {
            var cell = Cells[r.Next(Cells.GetLength(0)), r.Next(Cells.GetLength(1))];
            if (cell.Empty())
                return cell;
        }
    }

    /// <summary>
    /// Returns the cell the player is currently in
    /// </summary>
    public CompositeCell? LocatePlayer()
    {
        for (var i = 0; i <= Cells.GetUpperBound(0); i++)
        for (var j = 0; j <= Cells.GetUpperBound(1); j++)
            if (Cells[i, j].ContainsPlayer())
                return Cells[i, j];
        return null;
    }
}