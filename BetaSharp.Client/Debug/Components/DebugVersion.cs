using System.ComponentModel;
using BetaSharp.Client.UI;
using BetaSharp.Client.UI.Controls;

namespace BetaSharp.Client.Debug.Components;

[DisplayName("Version")]
[Description("Shows the current BetaSharp version.")]
public class DebugVersion : DebugComponent
{
    public DebugVersion() { }

    public override void AddRows(UIElement column, DebugContext ctx)
    {
        column.AddChild(new DebugRow("BetaSharp " + BetaSharp.Version));
    }

    public override DebugComponent Duplicate()
    {
        return new DebugVersion()
        {
            Right = Right
        };
    }
}
