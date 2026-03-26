using BetaSharp.Client.Input;
using BetaSharp.Client.Options;
using BetaSharp.Client.UI.Controls;
using BetaSharp.Client.UI.Layout.Flexbox;

namespace BetaSharp.Client.UI.Screens;

public class ControllerControlsScreen : BaseOptionsScreen
{
    public ControllerControlsScreen(UIScreen? parent, GameOptions options)
        : base(parent, options, "Controller Settings")
    {
        TitleText = "Controller Settings";
    }

    protected override IEnumerable<GameOption> GetOptions() => [];

    protected override UIElement CreateContent()
    {
        Panel container = new();
        container.Style.AlignItems = Align.Center;
        container.Style.MarginTop = 20;

        // Sensitivity & Type row
        Panel row1 = new();
        row1.Style.FlexDirection = FlexDirection.Row;
        row1.Style.JustifyContent = Justify.Center;
        row1.Style.Width = 340;

        UIElement sens = CreateControlForOption(Options.ControllerSensitivityOption);
        sens.Style.Width = 150;
        sens.Style.SetMargin(5);
        row1.AddChild(sens);

        UIElement type = CreateControlForOption(Options.ControllerTypeOption);
        type.Style.Width = 150;
        type.Style.SetMargin(5);
        row1.AddChild(type);
        container.AddChild(row1);

        // Bindings and Reset
        Panel row2 = new();
        row2.Style.FlexDirection = FlexDirection.Row;
        row2.Style.JustifyContent = Justify.Center;
        row2.Style.Width = 340;
        row2.Style.MarginTop = 10;

        Button btnBindings = new() { Text = "Edit Bindings..." };
        btnBindings.Style.Width = 150;
        btnBindings.Style.SetMargin(5);
        btnBindings.OnClick += (e) =>
        {
            Game.displayGuiScreen(new UIScreenAdapter(new ControllerBindingsScreen(this, Options)));
        };
        row2.AddChild(btnBindings);

        Button btnReset = new() { Text = "Reset Bindings" };
        btnReset.Style.Width = 150;
        btnReset.Style.SetMargin(5);
        btnReset.OnClick += (e) =>
        {
            foreach (ControllerBinding cb in Options.ControllerBindings)
                cb.Button = cb.DefaultButton;
            Options.SaveOptions();
        };
        row2.AddChild(btnReset);
        container.AddChild(row2);

        return container;
    }
}
