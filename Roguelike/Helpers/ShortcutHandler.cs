using SharpHook;
using SharpHook.Native;

namespace Roguelike.Helpers;

public static class ShortcutHandler
{
    private static readonly HashSet<KeyCode> KeysPressed = new();

    static ShortcutHandler()
    {
        var hook = new SimpleGlobalHook();
        hook.KeyPressed += (_, args) => { KeysPressed.Add(args.Data.KeyCode); };
        hook.KeyReleased += (_, args) => { KeysPressed.Remove(args.Data.KeyCode); };
        Task.Run(hook.Run);
    }

    public static bool IsPressed(params KeyCode[] keys)
    {
        return keys.All(x => KeysPressed.Contains(x));
    }
}