using System.ComponentModel;
using BetaSharp.Client.UI;
using BetaSharp.Client.UI.Controls;

namespace BetaSharp.Client.Debug.Components;

[DisplayName("Memory")]
[Description("Shows memory/GC info.")]
public class DebugMemory : DebugComponent
{
    public DebugMemory() { }

    public override void AddRows(UIElement column, DebugContext ctx)
    {
        long maxMem = ctx.GCMonitor.MaxMemoryBytes;
        long usedMem = ctx.GCMonitor.UsedMemoryBytes;
        long heapMem = ctx.GCMonitor.UsedHeapBytes;

        column.AddChild(new DebugRow($"Mem: {FormatPercentage(usedMem, maxMem)} {FormatMegabytes(usedMem)}/{FormatMegabytes(maxMem)}MB"));
        column.AddChild(new DebugRow($"Allocated: {FormatPercentage(heapMem, maxMem)} {FormatMegabytes(heapMem)}MB"));
    }

    public override DebugComponent Duplicate()
    {
        return new DebugMemory()
        {
            Right = Right
        };
    }

    private static string FormatMegabytes(long bytes)
    {
        return bytes <= 0L ? "N/A" : $"{bytes / 1024L / 1024L}";
    }

    private static string FormatPercentage(long value, long total)
    {
        return total > 0L ? $"{value * 100L / total}%" : "N/A";
    }
}
