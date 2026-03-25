using BetaSharp.Client.Guis;
using BetaSharp.Client.UI.Rendering;

namespace BetaSharp.Client.UI.Controls;

public class ServerListItem : UIElement
{
    public ServerData Data { get; }
    public bool IsSelected { get; set; }

    public ServerListItem(ServerData data)
    {
        Data = data;
        Style.Width = null; // Fill parent width
        Style.Height = 32;
        Style.SetPadding(4);
        Style.MarginBottom = 4;
        Style.MarginRight = 10;

        OnMouseEnter += (e) => IsHovered = true;
        OnMouseLeave += (e) => IsHovered = false;
    }

    public override void Render(UIRenderer renderer)
    {
        if (IsSelected)
        {
            renderer.DrawRect(0, 0, ComputedWidth, ComputedHeight, Color.Gray90);
            renderer.DrawRect(1, 1, ComputedWidth - 2, ComputedHeight - 2, Color.Black);
        }
        else
        {
            renderer.DrawRect(0, 0, ComputedWidth, ComputedHeight, Color.Gray40);
            renderer.DrawRect(1, 1, ComputedWidth - 2, ComputedHeight - 2, Color.Black);
        }

        renderer.DrawText(Data.Name, 4, 4, Color.White);

        string secondary = string.IsNullOrEmpty(Data.Motd) ? Data.Ip : Data.Motd;
        renderer.DrawText(secondary, 4, 16, Color.GrayA0);

        base.Render(renderer);
    }
}
