namespace BetaSharp.Client.Guis.Debug;

public class DebugVersion : DebugComponent
{
    public DebugVersion()
    {
    }

    public override void Draw(DebugContext ctx)
    {
        ctx.String("BetaSharp 1.7.3");
    }
}
