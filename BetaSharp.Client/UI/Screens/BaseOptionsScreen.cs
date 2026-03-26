using BetaSharp.Client.Guis;
using BetaSharp.Client.Options;
using BetaSharp.Client.UI.Controls;
using BetaSharp.Client.UI.Layout.Flexbox;

namespace BetaSharp.Client.UI.Screens;

public abstract class BaseOptionsScreen(UIScreen? parent, GameOptions options, string titleKey) : UIScreen(parent?.Game ?? BetaSharp.Instance)
{
    protected readonly UIScreen? Parent = parent;
    protected readonly GameOptions Options = options;
    protected string TitleText = TranslationStorage.Instance.TranslateKey(titleKey);

    protected override void Init()
    {
        Root.Style.AlignItems = Align.Center;
        Root.Style.JustifyContent = Justify.FlexStart;

        Root.AddChild(new Background(Game.world != null ? BackgroundType.World : BackgroundType.Dirt));

        Label title = new()
        {
            Text = TitleText,
            TextColor = Color.White,
            Centered = true
        };
        title.Style.MarginTop = 20;
        title.Style.MarginBottom = 12;
        Root.AddChild(title);

        UIElement content = CreateContent();
        Root.AddChild(content);

        Panel spacer = new();
        spacer.Style.FlexGrow = 1;
        Root.AddChild(spacer);

        Button btnDone = new() { Text = TranslationStorage.Instance.TranslateKey("gui.done") };
        btnDone.Style.MarginBottom = 20;
        btnDone.OnClick += (e) => OnDone();
        Root.AddChild(btnDone);
    }

    protected virtual UIElement CreateContent()
    {
        Panel grid = new();
        grid.Style.FlexDirection = FlexDirection.Row;
        grid.Style.FlexWrap = Wrap.Wrap;
        grid.Style.JustifyContent = Justify.Center;
        grid.Style.Width = 340;
        grid.Style.MarginTop = 20;

        foreach (GameOption option in GetOptions())
        {
            UIElement control = CreateControlForOption(option);
            control.Style.SetMargin(2);
            control.Style.Width = 150;
            grid.AddChild(control);
        }

        return grid;
    }

    protected abstract IEnumerable<GameOption> GetOptions();

    protected virtual void OnDone()
    {
        Options.SaveOptions();
        if (Parent != null)
        {
            Game.displayGuiScreen(new UIScreenAdapter(Parent));
        }
        else
        {
            Game.displayGuiScreen(null);
        }
    }

    protected virtual UIElement CreateControlForOption(GameOption option)
    {
        TranslationStorage translations = TranslationStorage.Instance;

        if (option is FloatOption floatOpt)
        {
            Slider slider = new()
            {
                Value = floatOpt.Value,
                Text = option.GetDisplayString(translations)
            };
            slider.OnValueChanged += (v) =>
            {
                floatOpt.Value = v;
                slider.Text = option.GetDisplayString(translations);
            };
            return slider;
        }
        else
        {
            Button btn = new() { Text = option.GetDisplayString(translations) };
            btn.OnClick += (e) =>
            {
                if (option is BoolOption boolOpt) boolOpt.Toggle();
                else if (option is CycleOption cycleOpt) cycleOpt.Cycle();

                btn.Text = option.GetDisplayString(translations);
            };
            return btn;
        }
    }
}
