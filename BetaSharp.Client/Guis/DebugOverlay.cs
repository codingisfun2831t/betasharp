using System.Runtime.InteropServices;
using BetaSharp.Util;
using BetaSharp.Util.Maths;
using SixLabors.Fonts;
using TextRenderer = BetaSharp.Client.Rendering.TextRenderer;

namespace BetaSharp.Client.Guis;

public class DebugOverlay
{
    private readonly BetaSharp _game;
    private readonly GCMonitor _gcMonitor;

    public DebugOverlay(BetaSharp game, GCMonitor gcMonitor)
    {
        _game = game;
        _gcMonitor = gcMonitor;
    }

    public void DrawDebug()
    {
        ScaledResolution scaled = new(_game.options, _game.displayWidth, _game.displayHeight);
        int scaledWidth = scaled.ScaledWidth;
        int scaledHeight = scaled.ScaledHeight;
        TextRenderer font = _game.fontRenderer;
        int y = 2;

        void leftString(string str, Color? clr = null)
        {
            font.DrawStringWrapped(str, 2, y, scaledWidth / 2, clr ?? Color.White);

            y += font.GetStringHeight(str, scaledWidth / 2);
        }

        void sep()
        {
            y += 10;
        }

        void rightString(string str, Color? clr = null)
        {
            font.DrawStringWrapped(str, scaledWidth / 2, y, scaledWidth / 2, clr ?? Color.White, HorizontalAlignment.Right);
            y += font.GetStringHeight(str, scaledWidth / 2);
        }

        leftString("Minecraft Beta 1.7.3 (" + _game.debug + ")");
        leftString(_game.getEntityDebugInfo());
        leftString(_game.getParticleAndEntityCountDebugInfo());
        leftString(_game.getWorldDebugInfo());
        sep();
        leftString("x: " + _game.player.x, Color.GrayE0);
        leftString("y: " + _game.player.y, Color.GrayE0);
        leftString("z: " + _game.player.z, Color.GrayE0);
        leftString("f: " + (MathHelper.Floor((double)(_game.player.yaw * 4.0F / 360.0F) + 0.5D) & 3), Color.GrayE0);

        if (_game.internalServer != null)
        {
            sep();
            leftString($"Server TPS: {_game.internalServer.Tps:F1}", Color.GrayE0);
        }

        y = 2;
        long maxMem = _gcMonitor.MaxMemoryBytes;
        long usedMem = _gcMonitor.UsedMemoryBytes;
        long heapMem = _gcMonitor.UsedHeapBytes;
        rightString("BetaSharp (" + RuntimeInformation.FrameworkDescription + ") running on " + RuntimeInformation.OSDescription);
        rightString("Used memory: " + usedMem * 100L / maxMem + "% (" + usedMem / 1024L / 1024L + "MB) of " + maxMem / 1024L / 1024L + "MB", Color.GrayE0);
        rightString("GC heap: " + heapMem * 100L / maxMem + "% (" + heapMem / 1024L / 1024L + "MB)", Color.GrayE0);
    }
}
