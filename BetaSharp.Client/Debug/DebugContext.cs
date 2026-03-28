using BetaSharp.Util;

namespace BetaSharp.Client.Debug;

public class DebugContext
{
    public readonly BetaSharp Game;
    public readonly GCMonitor GCMonitor;

    public DebugContext(BetaSharp game)
    {
        Game = game;
        GCMonitor = new GCMonitor();
    }
}
