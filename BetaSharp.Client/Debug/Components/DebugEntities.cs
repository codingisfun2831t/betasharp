using System.ComponentModel;
using BetaSharp.Client.Rendering;
using BetaSharp.Client.UI;
using BetaSharp.Client.UI.Controls;

namespace BetaSharp.Client.Debug.Components;

[DisplayName("Entities")]
[Description("Shows entities stats.")]
public class DebugEntities : DebugComponent
{
    public DebugEntities() { }

    public override void AddRows(UIElement column, DebugContext ctx)
    {
        WorldRenderer render = ctx.Game.terrainRenderer;
        column.AddChild(new DebugRow("Rendered Entities: " + render.countEntitiesRendered + "/" + render.countEntitiesTotal));
        column.AddChild(new DebugRow("Hidden Entities: " + render.countEntitiesHidden + ", Not in view: " + (render.countEntitiesTotal - render.countEntitiesHidden - render.countEntitiesRendered)));
    }

    public override DebugComponent Duplicate()
    {
        return new DebugEntities()
        {
            Right = Right
        };
    }
}
