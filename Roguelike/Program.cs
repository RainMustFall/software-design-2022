using System.Text;
using NStack;
using Roguelike.Core;
using Roguelike.Core.Abstractions.Map;
using Roguelike.Playables;
using Roguelike.Views;
using Terminal.Gui;

namespace Roguelike;

internal static class Program
{
    private static View AddMapView(View? container, Map.Map map, ICell playerCell)
    {
        var mapView = new MapView(map, playerCell)
        {
            X = 1,
            Y = 1,
            Width = Dim.Percent(70),
            Height = Dim.Fill()
        };

        container?.Add(mapView);
        return mapView;
    }

    private static View AddCharacterView(View? container, Game game, View anchor)
    {
        var charView = new CharacterView(game.GameController.InventoryEquipmentController, game.MainCharacter)
        {
            X = Pos.Right(anchor) + 1,
            Y = 1,
            Width = Dim.Fill() - 1,
            Height = Dim.Fill()
        };

        container?.Add(charView);
        return charView;
    }

    private static Label? ml;
    private static MenuBarItem? miScrollViewCheck;
    private static bool isBox10X = true;
    private static Window? win;
    private static ScrollView? scrollView;

    private static void Main(string[] args)
    {
        if (args.Length > 0 && args.Contains("-usc")) Application.UseSystemConsole = true;

        Console.OutputEncoding = Encoding.Default;

        Application.Init();

        var top = Application.Top;

        win = new Window("Roguelike")
        {
            X = 0,
            Y = 1,
            Width = Dim.Fill(),
            Height = Dim.Fill() - 1
        };

        var game = new Game();
        var mapView = AddMapView(win, game.GameController.MapController.Map, game.MainCharacter.Cell);
        AddCharacterView(win, game, mapView);
        top.Add(win);

        Application.MainLoop.AddTimeout(TimeSpan.FromMilliseconds(200), _ => game.MakeIteration());

        Application.Run();
        Application.Shutdown();
    }
}