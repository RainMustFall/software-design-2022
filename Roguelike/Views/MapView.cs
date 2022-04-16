using Terminal.Gui;

namespace Roguelike.Views;

using Map = Map.Map;

public class MapView : View
{
    private Map map;

    public MapView(Rect rect, Map map) : base(rect)
    {
        this.map = map;
    }

    public override void Redraw(Rect region)
    {
        Driver.SetAttribute(ColorScheme.Focus);
        var playerCell = map.LocatePlayer();
        if (playerCell == null)
            throw new Exception("No player found on the map");

        var f = Frame;

        int mapHeight = map.Cells.GetUpperBound(0);
        int mapWidth = map.Cells.GetUpperBound(1);

        int centerX = Math.Min(Math.Max(f.Width / 2, playerCell.X), mapWidth - f.Width / 2);
        int centerY = Math.Min(Math.Max(f.Height / 2, playerCell.Y), mapHeight - f.Height / 2);

        for (var y = 0; y < f.Height; y++)
        {
            Move(0, y);
            for (var x = 0; x < f.Width; x++)
            {
                var r = map.Cells[y + centerY - f.Height / 2, x + centerX - f.Width / 2].Render();
                Driver.AddRune(r);
            }
        }
    }
}