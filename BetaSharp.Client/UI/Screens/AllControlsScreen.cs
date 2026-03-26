using BetaSharp.Client.Options;
using BetaSharp.Client.UI.Controls;
using BetaSharp.Client.UI.Layout.Flexbox;

namespace BetaSharp.Client.UI.Screens;

public class AllControlsScreen : BaseOptionsScreen
{
    public AllControlsScreen(UIScreen? parent, GameOptions options) 
        : base(parent, options, "options.controls")
    {
        TitleText = "Controls";
    }

    protected override IEnumerable<GameOption> GetOptions() => [];

    protected override UIElement CreateContent()
    {
        Panel container = new();
        container.Style.AlignItems = Align.Center;
        container.Style.MarginTop = 20;

        Button btnKeyboard = new() { Text = "Keyboard Controls..." };
        btnKeyboard.Style.Width = 200;
        btnKeyboard.Style.MarginBottom = 10;
        btnKeyboard.OnClick += (e) =>
        {
            Game.displayGuiScreen(new UIScreenAdapter(new ControlsScreen(this, Options)));
        };
        container.AddChild(btnKeyboard);

        Button btnController = new() { Text = "Controller Controls..." };
        btnController.Style.Width = 200;
        btnController.OnClick += (e) =>
        {
            Game.displayGuiScreen(new UIScreenAdapter(new ControllerControlsScreen(this, Options)));
        };
        container.AddChild(btnController);

        return container;
    }
}
