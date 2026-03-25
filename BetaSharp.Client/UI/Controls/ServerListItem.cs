using BetaSharp.Client.Guis;
using BetaSharp.Client.UI.Rendering;

namespace BetaSharp.Client.UI.Controls;

public class ServerListItem(ServerData data) : ListItem<ServerData>(data)
{
    public override void Render(UIRenderer renderer)
    {
        base.Render(renderer);

        renderer.DrawText(Value.Name, 8, 4, Color.White);

        string secondary = string.IsNullOrEmpty(Value.Motd) ? Value.Ip : Value.Motd;
        renderer.DrawText(secondary, 8, 16, Color.GrayA0);
    }
}
