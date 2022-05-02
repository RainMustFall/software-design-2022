using SharpHook;
using SharpHook.Native;

namespace Roguelike.Helpers;

internal static class ShortcutHandler
{
    private static Dictionary<KeyCode, bool> keysPressed = new();

    static ShortcutHandler()
    {
        var hook = new SimpleGlobalHook();
        hook.KeyPressed += (_, args) => { keysPressed[args.Data.KeyCode] = true; };
        hook.KeyReleased += (_, args) => { keysPressed[args.Data.KeyCode] = false; };
        Task.Run(hook.Run);
    }

    public static bool IsPressed(params KeyCode[] keys)
    {
        return keys.All(x => keysPressed.ContainsKey(x));
    }

    public static void UpdateState()
    {
        keysPressed = keysPressed
            .Where(x => x.Value)
            .ToDictionary(x => x.Key, x => x.Value);
    }
}