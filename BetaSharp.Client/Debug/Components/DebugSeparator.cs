using System.ComponentModel;
using BetaSharp.Client.UI;
using BetaSharp.Client.UI.Controls;

namespace BetaSharp.Client.Debug.Components;

[DisplayName("Separator")]
[Description("Visual separator between components.")]
public class DebugSeparator : DebugComponent
{
    public DebugSeparator() { }

    public override void AddRows(UIElement column, DebugContext ctx)
    {
        column.AddChild(DebugRow.Spacer());
    }

    public override DebugComponent Duplicate()
    {
        return new DebugSeparator()
        {
            Right = Right
        };
    }
}
