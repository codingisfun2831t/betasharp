using System.Diagnostics;
using System.Runtime.InteropServices;
using BetaSharp.Client.Guis;
using BetaSharp.Client.Options;
using BetaSharp.Client.UI.Controls;
using BetaSharp.Client.UI.Controls.Core;
using BetaSharp.Client.UI.Layout.Flexbox;
using SixLabors.ImageSharp.Drawing.Processing;

namespace BetaSharp.Client.UI.Screens.Menu.Options;

public class CreditsScreen(UIContext context, UIScreen parent) : UIScreen(context)
{
    protected override void Init()
    {
        Root.Style.AlignItems = Align.Center;
        Root.Style.JustifyContent = Justify.FlexStart;

        Root.AddChild(new Background(Context.HasWorld ? BackgroundType.World : BackgroundType.Dirt));

        Label title = new()
        {
            Text = Translations.Get("menu.credits"),
            TextColor = Color.White,
            Centered = true
        };
        title.Style.MarginTop = 20;
        title.Style.MarginBottom = 8;
        Root.AddChild(title);
        AddTitleSpacer();

        ScrollView scroll = new();
        scroll.Style.Width = 300;
        scroll.Style.FlexGrow = 1;
        scroll.Style.MaxHeight = 200;
        scroll.Style.MarginBottom = 10;

        Content(scroll);

        Root.AddChild(scroll);

        Panel buttons = new();
        buttons.Style.FlexDirection = FlexDirection.Row;

        Button btnDone = CreateButton();
        btnDone.Text = Translations.GetFormatted("gui.done");
        btnDone.Style.MarginBottom = 20;
        btnDone.Style.MarginRight = 4;
        btnDone.OnClick += (e) => Context.Navigator.Navigate(parent);
        buttons.AddChild(btnDone);

        ImageButton btnLang = CreateImageButton();
        btnLang.OnClick += (e) => Context.Navigator.Navigate(new TranslationsCreditScreen(Context, this));
        btnLang.Texture = Renderer.TextureManager.GetTextureId("/gui/Globe.png");
        btnLang.U = 0;
        btnLang.V = 0;
        btnLang.UWidth = 24;
        btnLang.VHeight = 24;
        btnLang.Style.MarginBottom = 20;
        buttons.AddChild(btnLang);

        Root.AddChild(buttons);
    }

    private void Content(ScrollView scroll)
    {
        void ColoredText(string text, Color color, float scale)
        {
            Label lbl = new Label()
            {
                Text = text,
                Scale = scale,
                TextColor = color,
                Centered = true,
            };

            lbl.Style.MarginBottom = 4;
            scroll.AddContent(lbl);
        }

        void Text(string text, float scale = 1.0F)
            => ColoredText(text, Color.White, scale);

        void Header(string text)
            => ColoredText(text, Color.Yellow, 1f);

        void Link(string text, string url, float scale = 1.0F)
        {
            Link lbl = new Link()
            {
                Text = text,
                Scale = scale,
                Centered = true,
                URL = url
            };

            lbl.Style.MarginBottom = 4;
            scroll.AddContent(lbl);
        }

        void Seperator() {
            var ele = new UIElement();
            ele.Style.Height = 10;
            scroll.AddContent(ele);
        }

        const int scale = 20;
        const int imageWidth = 1000 / scale;
        const int imageHeight = 675 / scale;

        var image = new Image();
        image.Texture = Context.TextureManager.GetTextureId("gui/Logo.png");
        image.Style.Width = imageWidth;
        image.Style.Height = imageHeight;
        image.Style.AlignSelf = Align.Center;
        image.Style.MarginBottom = 10;
        scroll.AddContent(image);

        Header(Translations.GetFormatted("credits.version", BetaSharp.Version));
        Text(Translations.Get("credits.description"));
        Link(Translations.Get("credits.github"), "https://github.com/betasharp-official/betasharp/");
        Link(Translations.Get("credits.madeBy"), "https://github.com/Fazin85");
        Seperator();

        Header(Translations.Get("credits.libraries"));
        Link(Translations.Get("credits.sliknet"), "https://github.com/dotnet/Silk.NET");
        Link(Translations.Get("credits.imgui"), "https://github.com/ocornut/imgui");
        Link(Translations.Get("credits.sfml"), "https://github.com/SFML/SFML.Net");
        Link(Translations.Get("credits.sixlabors"), "https://github.com/sixlabors");
    }

    
}
