using BetaSharp.Util;

namespace BetaSharp.Client.Guis.Debug;

public class DebugOverlay
{
    private readonly DebugContext _debugContext;

    public List<DebugComponent> Components { get; } = new();

    public DebugOverlay(BetaSharp game)
    {
        _debugContext = new(game);
    }

    public void DrawDebug()
    {
        _debugContext.Inititalize();

        foreach (DebugComponent component in Components)
        {
            _debugContext.DrawComponent(component);
        }

        // leftString("Minecraft Beta 1.7.3 (" + _game.debug + ")");
        // leftString(_game.getEntityDebugInfo());
        // leftString(_game.getParticleAndEntityCountDebugInfo());
        // leftString(_game.getWorldDebugInfo());
        // sep();
        // leftString("x: " + _game.player.x, Color.GrayE0);
        // leftString("y: " + _game.player.y, Color.GrayE0);
        // leftString("z: " + _game.player.z, Color.GrayE0);
        // leftString("f: " + (MathHelper.Floor((double)(_game.player.yaw * 4.0F / 360.0F) + 0.5D) & 3), Color.GrayE0);
        //
        // if (_game.internalServer != null)
        // {
        //     sep();
        //     leftString($"Server TPS: {_game.internalServer.Tps:F1}", Color.GrayE0);
        // }
        //
        // y = 2;
        // long maxMem = _gcMonitor.MaxMemoryBytes;
        // long usedMem = _gcMonitor.UsedMemoryBytes;
        // long heapMem = _gcMonitor.UsedHeapBytes;
        // rightString("BetaSharp (" + RuntimeInformation.FrameworkDescription + ") running on " + RuntimeInformation.OSDescription);
        // rightString("Used memory: " + usedMem * 100L / maxMem + "% (" + usedMem / 1024L / 1024L + "MB) of " + maxMem / 1024L / 1024L + "MB", Color.GrayE0);
        // rightString("GC heap: " + heapMem * 100L / maxMem + "% (" + heapMem / 1024L / 1024L + "MB)", Color.GrayE0);
    }
}
