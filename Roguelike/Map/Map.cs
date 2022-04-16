using Roguelike.Map.Cells;

namespace Roguelike.Map;

public class Map
{
    public Map(int width, int height)
    {
        Cells = new CompositeCell[width, height];
        for (var i = 0; i < height; ++i)
        for (var j = 0; j < width; ++j)
            Cells[i, j] = new CompositeCell(j, i);
        GenerateMap();
    }

    public CompositeCell[,] Cells { get; }

    private void GenerateMap(double placementThreshold = 0.6)
    {
        Random random = new Random();
        int rMax = Cells.GetUpperBound(0);
        int cMax = Cells.GetUpperBound(1);

        for (int i = 0; i <= rMax; i++)
        for (int j = 0; j <= cMax; j++)
            if (i == 0 || j == 0 || i == rMax || j == cMax)
                Cells[i, j].PutCell(new WallCell(i, j));
            else if (i % 2 == 0 && j % 2 == 0)
            {
                var nextDouble = random.NextDouble();
                if (nextDouble > placementThreshold)
                {
                    Cells[i, j].PutCell(new WallCell(i, j));

                    int a = nextDouble < .5 ? 0 : (nextDouble < .5 ? -1 : 1);
                    int b = a != 0 ? 0 : (nextDouble < .5 ? -1 : 1);
                    Cells[i + a, j + b].PutCell(new WallCell(i, j));
                }
            }
    }

    public CompositeCell? GetFirstEmptyCell()
    {
        for (int i = 0; i <= Cells.GetUpperBound(0); i++)
        for (int j = 0; j <= Cells.GetUpperBound(1); j++)
            if (Cells[i, j].Empty())
                return Cells[i, j];
        return null;
    }

    public CompositeCell? LocatePlayer()
    {
        for (int i = 0; i <= Cells.GetUpperBound(0); i++)
        for (int j = 0; j <= Cells.GetUpperBound(1); j++)
            if (Cells[i, j].IsPlayer())
                return Cells[i, j];
        return null;
    }
}