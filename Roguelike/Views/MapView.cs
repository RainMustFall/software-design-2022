using Roguelike.Core.Abstractions.Map;
using Terminal.Gui;

namespace Roguelike.Views;

using Map = Map.Map;

/// <summary>
/// Class <c>CharacterView</c> is responsible for displaying the state of
/// the map
/// </summary>
public class MapView : View
{
    private readonly Map map;
    private readonly ICell playerCell;

    public MapView(Map map, ICell playerCell)
    {
        this.map = map;
        this.playerCell = playerCell;
    }

    public override void Redraw(Rect region)
    {
        Driver.SetAttribute(ColorScheme.Focus);

        var f = Frame;

        var mapHeight = map.Cells.GetUpperBound(1);
        var mapWidth = map.Cells.GetUpperBound(0);

        var centerX = Math.Min(Math.Max(f.Width / 2, playerCell.X), mapWidth - f.Width / 2);
        var centerY = Math.Min(Math.Max(f.Height / 2, playerCell.Y), mapHeight - f.Height / 2);

        for (var y = 0; y < f.Height; y++)
        {
            Move(0, y);
            for (var x = 0; x < f.Width; x++)
            {
                var row = y + centerY - f.Height / 2;
                var col = x + centerX - f.Width / 2;

                if (row < mapHeight && col < mapWidth && row >= 0 && col >= 0)
                {
                    var r = map.Cells[col, row].Render();
                    Driver.AddRune(r);
                }
            }
        }
    }
}