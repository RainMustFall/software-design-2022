using System.Text;
using NStack;
using Roguelike.Core;
using Roguelike.Playables;
using Roguelike.Views;
using Terminal.Gui;

namespace Roguelike;

internal static class Demo
{
    private static View AddMapView(View? container, Map.Map map)
    {
        var mapView = new MapView(map)
        {
            X = 1,
            Y = 1,
            Width = Dim.Percent(70),
            Height = Dim.Fill()
        };
        
        container?.Add(mapView);
        return mapView;
    }

    private static View AddCharacterView(View? container, ProgressibleHumanoid character, View anchor)
    {
        
        var charView = new CharacterView(character)
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

        win = new Window("Rougelike")
        {
            X = 0,
            Y = 1,
            Width = Dim.Fill(),
            Height = Dim.Fill() - 1
        };
        
        var game = new Game();
        var mapView = AddMapView(win, game.map);
        AddCharacterView(win, game.character, mapView);
        top.Add(win);

        Application.Run();
        game.Run();
        Application.Shutdown();
    }
}