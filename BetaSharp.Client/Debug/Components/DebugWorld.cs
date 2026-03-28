using System.ComponentModel;
using BetaSharp.Client.UI;
using BetaSharp.Client.UI.Controls;

namespace BetaSharp.Client.Debug.Components;

[DisplayName("World Info")]
[Description("Shows world debug info.")]
public class DebugWorld : DebugComponent
{
    public DebugWorld() { }

    public override void AddRows(UIElement column, DebugContext ctx)
    {
        column.AddChild(new DebugRow(ctx.Game.getWorldDebugInfo()));
    }

    public override DebugComponent Duplicate()
    {
        return new DebugWorld()
        {
            Right = Right
        };
    }
}
