using BetaSharp.Client.Options;
using BetaSharp.Client.UI.Controls;
using BetaSharp.Client.UI.Layout.Flexbox;

namespace BetaSharp.Client.UI.Screens;

public class OptionsScreen(UIScreen? parent, GameOptions options) : BaseOptionsScreen(parent, options, "options.title")
{
    protected override IEnumerable<GameOption> GetOptions() => Options.MainScreenOptions;

    protected override UIElement CreateContent()
    {
        Panel container = new();
        container.Style.AlignItems = Align.Center;

        // Main options grid
        UIElement grid = base.CreateContent();
        container.AddChild(grid);

        // Sub-menu buttons
        TranslationStorage translations = TranslationStorage.Instance;
        Panel subMenuGrid = new();
        subMenuGrid.Style.FlexDirection = FlexDirection.Row;
        subMenuGrid.Style.FlexWrap = Wrap.Wrap;
        subMenuGrid.Style.JustifyContent = Justify.Center;
        subMenuGrid.Style.Width = 340;
        subMenuGrid.Style.MarginTop = 10;

        Button btnVideo = new() { Text = translations.TranslateKey("options.video") };
        btnVideo.Style.SetMargin(2);
        btnVideo.Style.Width = 150;
        btnVideo.OnClick += (e) =>
        {
            Options.SaveOptions();
            Game.displayGuiScreen(new UIScreenAdapter(new VideoSettingsScreen(this, Options)));
        };
        subMenuGrid.AddChild(btnVideo);

        Button btnDebug = new() { Text = "Debug Options..." };
        btnDebug.Style.SetMargin(2);
        btnDebug.Style.Width = 150;
        btnDebug.OnClick += (e) =>
        {
            Options.SaveOptions();
            Game.displayGuiScreen(new UIScreenAdapter(new DebugOptionsScreen(this, Options)));
        };
        subMenuGrid.AddChild(btnDebug);

        Button btnAudio = new() { Text = "Audio Settings" };
        btnAudio.Style.SetMargin(2);
        btnAudio.Style.Width = 150;
        btnAudio.OnClick += (e) =>
        {
            Options.SaveOptions();
            Game.displayGuiScreen(new UIScreenAdapter(new AudioSettingsScreen(this, Options)));
        };
        subMenuGrid.AddChild(btnAudio);

        Button btnControls = new() { Text = translations.TranslateKey("options.controls") };
        btnControls.Style.SetMargin(2);
        btnControls.Style.Width = 150;
        btnControls.OnClick += (e) =>
        {
            Options.SaveOptions();
            Game.displayGuiScreen(new UIScreenAdapter(new AllControlsScreen(this, Options)));
        };
        subMenuGrid.AddChild(btnControls);

        container.AddChild(subMenuGrid);
        return container;
    }
}
