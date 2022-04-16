using System.Text;
using NStack;
using Roguelike.Core;
using Roguelike.Views;
using Terminal.Gui;

namespace Roguelike;

using Rune = System.Rune;

internal static class Demo
{
    private static void AddMapView(View? container, Map.Map map)
    {
        container?.Add(new MapView(new Rect(0, 0, 80, 16), map));
    }

    private static void NewFile()
    {
        var okButton = new Button("Ok", true);
        okButton.Clicked += () => Application.RequestStop();
        var cancelButton = new Button("Cancel");
        cancelButton.Clicked += () => Application.RequestStop();

        var d = new Dialog(
            "New File", 50, 20,
            okButton,
            cancelButton);

        var ml2 = new Label(1, 1, "Mouse Debug Line");
        d.Add(ml2);
        Application.Run(d);
    }

    private static bool Quit()
    {
        var n = MessageBox.Query(50, 7, "Quit Demo", "Are you sure you want to quit this demo?", "Yes", "No");
        return n == 0;
    }

    private static void Close()
    {
        MessageBox.ErrorQuery(50, 7, "Error", "There is nothing to close", "Ok");
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

        win = new Window("Hello")
        {
            X = 0,
            Y = 1,
            Width = Dim.Fill(),
            Height = Dim.Fill() - 1
        };
        
        var game = new Game();
        AddMapView(win, game.map);
        top.Add(win);
        
        Application.Run();
        game.Run();
        Application.Shutdown();
    }
}