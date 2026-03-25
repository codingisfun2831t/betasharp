using BetaSharp.Client.Rendering.Core.Textures;
using BetaSharp.Client.UI.Layout.Flexbox;
using BetaSharp.Client.UI.Rendering;

namespace BetaSharp.Client.UI.Controls;

public class Background : UIElement
{
    public string TexturePath { get; set; } = "/gui/background.png";
    public float Scale { get; set; } = 32.0f;

    public Background()
    {
        Style.Position = PositionType.Absolute;
        Style.Top = 0;
        Style.Left = 0;
        Style.Right = 0;
        Style.Bottom = 0;
    }

    public override void Render(UIRenderer renderer)
    {
        TextureHandle texture = renderer.TextureManager.GetTextureId(TexturePath);
        renderer.DrawRepeatingTexture(texture, 0, 0, ComputedWidth, ComputedHeight, Scale);
        base.Render(renderer);
    }
}
