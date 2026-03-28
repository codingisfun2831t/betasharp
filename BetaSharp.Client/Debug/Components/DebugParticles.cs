using System.ComponentModel;
using BetaSharp.Client.UI;
using BetaSharp.Client.UI.Controls;

namespace BetaSharp.Client.Debug.Components;

[DisplayName("Particles")]
[Description("Shows particle stats.")]
public class DebugParticles : DebugComponent
{
    public DebugParticles() { }

    public override void AddRows(UIElement column, DebugContext ctx)
    {
        column.AddChild(new DebugRow(ctx.Game.getParticleDebugInfo()));
    }

    public override DebugComponent Duplicate()
    {
        return new DebugParticles()
        {
            Right = Right
        };
    }
}
