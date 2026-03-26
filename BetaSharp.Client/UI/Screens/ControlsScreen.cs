using BetaSharp.Client.Input;
using BetaSharp.Client.Options;
using BetaSharp.Client.UI.Controls;
using BetaSharp.Client.UI.Layout.Flexbox;

namespace BetaSharp.Client.UI.Screens;

public class ControlsScreen : BaseOptionsScreen
{
    private int _selectedKey = -1;

    public ControlsScreen(UIScreen? parent, GameOptions options)
        : base(parent, options, "controls.title")
    {
        TitleText = "Controls";
    }

    protected override IEnumerable<GameOption> GetOptions() => [];

    protected override UIElement CreateContent()
    {
        Panel container = new();
        container.Style.AlignItems = Align.Center;

        // Keybinds Grid
        Panel grid = new();
        grid.Style.FlexDirection = FlexDirection.Row;
        grid.Style.FlexWrap = Wrap.Wrap;
        grid.Style.JustifyContent = Justify.Center;
        grid.Style.Width = 340;
        grid.Style.MarginTop = 10;

        for (int i = 0; i < Options.KeyBindings.Length; i++)
        {
            int index = i;
            KeyBinding bind = Options.KeyBindings[i];

            Panel row = new();
            row.Style.FlexDirection = FlexDirection.Row;
            row.Style.AlignItems = Align.Center;
            row.Style.Width = 160;
            row.Style.SetMargin(2);

            Label label = new() { Text = Options.GetKeyBindingDescription(i) };
            label.Style.FlexGrow = 1;
            row.AddChild(label);

            string btnText = _selectedKey == index ? "> ??? <" : Options.GetOptionDisplayString(index);
            Button btn = new() { Text = btnText };
            btn.Style.Width = 70;
            btn.OnClick += (e) =>
            {
                _selectedKey = index;
                Refresh();
            };
            row.AddChild(btn);

            grid.AddChild(row);
        }
        container.AddChild(grid);

        // Sensitivity & Invert
        Panel footer = new();
        footer.Style.FlexDirection = FlexDirection.Row;
        footer.Style.JustifyContent = Justify.Center;
        footer.Style.Width = 340;
        footer.Style.MarginTop = 10;

        UIElement sensitivity = CreateControlForOption(Options.MouseSensitivityOption);
        sensitivity.Style.Width = 150;
        sensitivity.Style.SetMargin(2);
        footer.AddChild(sensitivity);

        UIElement invert = CreateControlForOption(Options.InvertMouseOption);
        invert.Style.Width = 150;
        invert.Style.SetMargin(2);
        footer.AddChild(invert);

        container.AddChild(footer);

        return container;
    }

    private void Refresh()
    {
        Root.Children.Clear();
        Init();
    }

    public override void KeyTyped(int key, char character)
    {
        if (_selectedKey >= 0)
        {
            Options.SetKeyBinding(_selectedKey, key);
            _selectedKey = -1;
            Refresh();
        }
        else
        {
            base.KeyTyped(key, character);
        }
    }
}
