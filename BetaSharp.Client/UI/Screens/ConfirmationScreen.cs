using BetaSharp.Client.Guis;
using BetaSharp.Client.UI.Controls;
using BetaSharp.Client.UI.Layout.Flexbox;

namespace BetaSharp.Client.UI.Screens;

public class ConfirmationScreen : UIScreen
{
    private readonly UIScreen _parent;
    private readonly string _title;
    private readonly string _message;
    private readonly string _confirmText;
    private readonly string _cancelText;
    private readonly Action<bool> _callback;

    public ConfirmationScreen(BetaSharp game, UIScreen parent, string title, string message, string confirmText, string cancelText, Action<bool> callback) : base(game)
    {
        _parent = parent;
        _title = title;
        _message = message;
        _confirmText = confirmText;
        _cancelText = cancelText;
        _callback = callback;
    }

    protected override void Init()
    {
        Root.AddChild(new Background());
        Root.Style.AlignItems = Align.Center;
        Root.Style.JustifyContent = Justify.Center;
        Root.Style.SetPadding(20);

        Label lblTitle = new() { Text = _title, TextColor = Color.White };
        lblTitle.Style.MarginBottom = 10;
        Root.AddChild(lblTitle);

        Label lblMsg = new() { Text = _message, TextColor = Color.GrayA0 };
        lblMsg.Style.MarginBottom = 20;
        Root.AddChild(lblMsg);

        Panel buttonPanel = new();
        buttonPanel.Style.FlexDirection = FlexDirection.Row;

        Button btnConfirm = new() { Text = _confirmText };
        btnConfirm.Style.Width = 100;
        btnConfirm.Style.SetMargin(0, 4, 0, 0);
        btnConfirm.OnClick += (e) =>
        {
            _callback(true);
            Game.displayGuiScreen(new UIScreenAdapter(_parent));
        };
        buttonPanel.AddChild(btnConfirm);

        Button btnCancel = new() { Text = _cancelText };
        btnCancel.Style.Width = 100;
        btnCancel.OnClick += (e) =>
        {
            _callback(false);
            Game.displayGuiScreen(new UIScreenAdapter(_parent));
        };
        buttonPanel.AddChild(btnCancel);

        Root.AddChild(buttonPanel);
    }
}
