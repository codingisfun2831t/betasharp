using System.ComponentModel;
using System.Runtime.InteropServices;
using BetaSharp.Client.UI;
using BetaSharp.Client.UI.Controls;

namespace BetaSharp.Client.Debug.Components;

[DisplayName("Framework")]
[Description("Shows .NET version.")]
public class DebugFramework : DebugComponent
{
    public DebugFramework() { }

    public override void AddRows(UIElement column, DebugContext ctx)
    {
        column.AddChild(new DebugRow(RuntimeInformation.FrameworkDescription));
    }

    public override DebugComponent Duplicate()
    {
        return new DebugFramework()
        {
            Right = Right
        };
    }
}
