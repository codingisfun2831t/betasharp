using BetaSharp.Client.Guis;
using BetaSharp.Client.UI.Controls;
using BetaSharp.Client.UI.Layout.Flexbox;

namespace BetaSharp.Client.UI.Screens;

public class EditServerScreen : UIScreen
{
    private readonly MultiplayerScreen _parent;
    private readonly ServerData _serverData;
    private readonly bool _isEditing;

    private TextField _txfName = null!;
    private TextField _txfAddress = null!;

    public EditServerScreen(BetaSharp game, MultiplayerScreen parent, ServerData serverData, bool isEditing) : base(game)
    {
        _parent = parent;
        _serverData = serverData;
        _isEditing = isEditing;
    }

    protected override void Init()
    {
        Root.AddChild(new Background());
        Root.Style.AlignItems = Align.Center;
        Root.Style.JustifyContent = Justify.Center;
        Root.Style.SetPadding(20);

        Label title = new() { Text = _isEditing ? "Edit Server Info" : "Add Server Info", TextColor = Color.White };
        title.Style.MarginBottom = 10;
        Root.AddChild(title);

        Label lName = new() { Text = "Server Name", TextColor = Color.GrayA0 };
        lName.Style.MarginBottom = 4;
        Root.AddChild(lName);

        _txfName = new TextField();
        _txfName.Style.Width = 200;
        _txfName.Style.MarginBottom = 10;
        _txfName.Text = _serverData.Name;
        Root.AddChild(_txfName);

        Label lAddr = new() { Text = "Server Address", TextColor = Color.GrayA0 };
        lAddr.Style.MarginBottom = 4;
        Root.AddChild(lAddr);

        _txfAddress = new TextField();
        _txfAddress.Style.Width = 200;
        _txfAddress.Style.MarginBottom = 20;
        _txfAddress.Text = _serverData.Ip;
        Root.AddChild(_txfAddress);

        Panel buttonPanel = new();
        buttonPanel.Style.FlexDirection = FlexDirection.Row;

        Button btnDone = new() { Text = "Done" };
        btnDone.Style.Width = 100;
        btnDone.Style.SetMargin(0, 4, 0, 0);
        btnDone.OnClick += (e) =>
        {
            _serverData.Name = _txfName.Text;
            _serverData.Ip = _txfAddress.Text;
            _parent.ConfirmEdit(_serverData, _isEditing);
            Game.displayGuiScreen(new UIScreenAdapter(_parent));
        };
        buttonPanel.AddChild(btnDone);

        Button btnCancel = new() { Text = "Cancel" };
        btnCancel.Style.Width = 100;
        btnCancel.OnClick += (e) => Game.displayGuiScreen(new UIScreenAdapter(_parent));
        buttonPanel.AddChild(btnCancel);

        Root.AddChild(buttonPanel);
    }
}
