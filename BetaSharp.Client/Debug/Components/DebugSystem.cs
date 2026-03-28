using System.ComponentModel;
using BetaSharp.Client.Diagnostics;
using BetaSharp.Client.UI;
using BetaSharp.Client.UI.Controls;

namespace BetaSharp.Client.Debug.Components;

[DisplayName("System")]
[Description("Shows info about your system.")]
public class DebugSystem : DebugComponent
{
    public DebugSystem() { }

    public override void AddRows(UIElement column, DebugContext ctx)
    {
        DebugSystemSnapshot systemSnapshot = ctx.Game.GetDebugSystemSnapshot();
        column.AddChild(new DebugRow($"CPU: {FormatCpuInfo(systemSnapshot)}"));
        column.AddChild(new DebugRow($"GPU: {systemSnapshot.GpuName} (VRAM: {systemSnapshot.GpuVram})"));
        column.AddChild(new DebugRow($"OpenGL: {systemSnapshot.OpenGlVersion}"));
        column.AddChild(new DebugRow($"GLSL: {systemSnapshot.GlslVersion}"));
        column.AddChild(new DebugRow($"Driver: {systemSnapshot.DriverVersion}"));
        column.AddChild(new DebugRow($"OS: {systemSnapshot.OsDescription}"));
        column.AddChild(new DebugRow($".NET: {systemSnapshot.DotNetRuntime}"));
    }

    private static string FormatCpuInfo(DebugSystemSnapshot systemSnapshot)
    {
        string coreLabel = systemSnapshot.CpuCoreCount == 1 ? "core" : "cores";
        if (systemSnapshot.CpuName == DebugTelemetry.UnknownValue)
        {
            return $"{systemSnapshot.CpuCoreCount} {coreLabel}";
        }

        return $"{systemSnapshot.CpuName} ({systemSnapshot.CpuCoreCount} {coreLabel})";
    }

    public override DebugComponent Duplicate()
    {
        return new DebugSystem()
        {
            Right = Right
        };
    }
}
