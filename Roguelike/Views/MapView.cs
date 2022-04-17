using Terminal.Gui;

namespace Roguelike.Views;

using Map = Map.Map;

/// <summary>
/// Class <c>CharacterView</c> is responsible for displaying the state of
/// the map
/// </summary>
public class MapView : View
{
    private Map map;

    public MapView(Map map)
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
                var row = y + centerY - f.Height / 2;
                var col = x + centerX - f.Width / 2;

                if (row < mapHeight && col < mapWidth && row >= 0 && col >= 0)
                {
                    var r = map.Cells[row, col].Render();
                    Driver.AddRune(r);
                }
            }
        }
    }
}