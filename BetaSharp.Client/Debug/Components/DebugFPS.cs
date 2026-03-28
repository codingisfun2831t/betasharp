using System.ComponentModel;
using BetaSharp.Client.UI;
using BetaSharp.Client.UI.Controls;

namespace BetaSharp.Client.Debug.Components;

[DisplayName("FPS")]
[Description("Shows the current FPS.")]
public class DebugFPS : DebugComponent
{
    public DebugFPS() { }

    public override void AddRows(UIElement column, DebugContext ctx)
    {
        column.AddChild(new DebugRow(ctx.Game.debug));
    }

    public override DebugComponent Duplicate()
    {
        return new DebugFPS()
        {
            Right = Right
        };
    }
}
