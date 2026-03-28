using System.ComponentModel;
using BetaSharp.Client.UI;
using BetaSharp.Client.UI.Controls;

namespace BetaSharp.Client.Debug.Components;

[DisplayName("Server")]
[Description("Shows server info.")]
public class DebugServer : DebugComponent
{
    public DebugServer() { }

    public override void AddRows(UIElement column, DebugContext ctx)
    {
        if (ctx.Game.internalServer != null)
        {
            column.AddChild(new DebugRow($"Integrated server @ {ctx.Game.internalServer.Tps:F1}/20 TPS"));
        }
    }

    public override DebugComponent Duplicate()
    {
        return new DebugServer()
        {
            Right = Right
        };
    }
}
