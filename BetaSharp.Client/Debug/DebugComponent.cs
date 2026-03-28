using BetaSharp.Client.UI;

namespace BetaSharp.Client.Debug;

public abstract class DebugComponent
{
    public bool Right { get; set; }
    public abstract void AddRows(UIElement column, DebugContext ctx);
    public abstract DebugComponent Duplicate();
}
