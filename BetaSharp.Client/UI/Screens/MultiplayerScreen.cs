using BetaSharp.Client.Guis;
using BetaSharp.Client.Rendering.Core.Textures;
using BetaSharp.Client.UI.Controls;
using BetaSharp.Client.UI.Layout.Flexbox;
using BetaSharp.Client.UI.Rendering;
using BetaSharp.NBT;

namespace BetaSharp.Client.UI.Screens;

public class MultiplayerScreen : UIScreen
{
    private readonly List<ServerData> _serverList = [];
    private ScrollView _scrollView = null!;
    private int _selectedServerIndex = -1;
    private readonly List<ServerListItem> _listItems = [];

    private Button _btnJoin = null!;
    private Button _btnEdit = null!;
    private Button _btnDelete = null!;

    public MultiplayerScreen(BetaSharp game) : base(game)
    {
    }

    protected override void Init()
    {
        Root.AddChild(new Background());
        LoadServerList();

        Root.Style.AlignItems = Align.Center;
        Root.Style.SetPadding(20);

        Label title = new() { Text = "Play Multiplayer", TextColor = Color.White };
        title.Style.MarginBottom = 10;
        Root.AddChild(title);

        _scrollView = new ScrollView();
        _scrollView.Style.Width = 300;
        _scrollView.Style.FlexGrow = 1;
        _scrollView.Style.MarginBottom = 10;
        Root.AddChild(_scrollView);

        PopulateServerList();

        Panel buttonPanel = new();
        buttonPanel.Style.FlexDirection = FlexDirection.Row;
        buttonPanel.Style.JustifyContent = Justify.Center;
        buttonPanel.Style.Width = 310;

        _btnJoin = new Button { Text = "Join Server" };
        _btnJoin.Style.Width = 150;
        _btnJoin.Style.SetMargin(2);
        _btnJoin.OnClick += (e) => ConnectSelected();
        buttonPanel.AddChild(_btnJoin);

        Button btnDirect = new Button { Text = "Direct Connect" };
        btnDirect.Style.Width = 150;
        btnDirect.Style.SetMargin(2);
        btnDirect.OnClick += (e) => Game.displayGuiScreen(new UIScreenAdapter(new DirectConnectScreen(Game, this, new ServerData("BetaSharp Server", ""))));
        buttonPanel.AddChild(btnDirect);

        Button btnAdd = new Button { Text = "Add Server" };
        btnAdd.Style.Width = 100;
        btnAdd.Style.SetMargin(2);
        btnAdd.OnClick += (e) => Game.displayGuiScreen(new UIScreenAdapter(new EditServerScreen(Game, this, new ServerData("BetaSharp Server", ""), false)));
        buttonPanel.AddChild(btnAdd);

        _btnEdit = new Button { Text = "Edit" };
        _btnEdit.Style.Width = 100;
        _btnEdit.Style.SetMargin(2);
        _btnEdit.OnClick += (e) => EditSelected();
        buttonPanel.AddChild(_btnEdit);

        _btnDelete = new Button { Text = "Delete" };
        _btnDelete.Style.Width = 100;
        _btnDelete.Style.SetMargin(2);
        _btnDelete.OnClick += (e) => DeleteSelected();
        buttonPanel.AddChild(_btnDelete);

        Button btnRefresh = new Button { Text = "Refresh" };
        btnRefresh.Style.Width = 150;
        btnRefresh.Style.SetMargin(2);
        btnRefresh.OnClick += (e) => { LoadServerList(); PopulateServerList(); };
        buttonPanel.AddChild(btnRefresh);

        Button btnCancel = new Button { Text = "Cancel" };
        btnCancel.Style.Width = 150;
        btnCancel.Style.SetMargin(2);
        btnCancel.OnClick += (e) => Game.displayGuiScreen(new UIScreenAdapter(new MainMenuScreen(Game)));
        buttonPanel.AddChild(btnCancel);

        Root.AddChild(buttonPanel);

        UpdateButtons();
    }

    private void LoadServerList()
    {
        try
        {
            string path = Path.Combine(BetaSharp.getBetaSharpDir(), "servers.dat");
            if (!File.Exists(path)) return;

            using FileStream stream = File.OpenRead(path);
            NBTTagCompound tag = NbtIo.ReadCompressed(stream);

            NBTTagList list = tag.GetTagList("servers");
            _serverList.Clear();
            for (int i = 0; i < list.TagCount(); ++i)
            {
                _serverList.Add(ServerData.FromNBT((NBTTagCompound)list.TagAt(i)));
            }
        }
        catch { }
    }

    private void SaveServerList()
    {
        try
        {
            NBTTagList list = new();
            foreach (ServerData server in _serverList)
            {
                list.SetTag(server.ToNBT());
            }
            NBTTagCompound tag = new();
            tag.SetTag("servers", list);

            string path = Path.Combine(BetaSharp.getBetaSharpDir(), "servers.dat");
            using FileStream stream = File.Create(path);
            NbtIo.WriteCompressed(tag, stream);
        }
        catch { }
    }

    private void PopulateServerList()
    {
        _scrollView.ContentContainer.Children.Clear();
        _listItems.Clear();
        _selectedServerIndex = -1;

        for (int i = 0; i < _serverList.Count; i++)
        {
            int index = i;
            ServerListItem item = new(_serverList[i]);
            item.OnClick += (e) => SelectServer(index);
            _scrollView.AddContent(item);
            _listItems.Add(item);
        }
    }

    private void SelectServer(int index)
    {
        _selectedServerIndex = index;
        foreach (var item in _listItems) item.IsSelected = false;
        if (index >= 0 && index < _listItems.Count) _listItems[index].IsSelected = true;
        UpdateButtons();
    }

    private void UpdateButtons()
    {
        bool hasSelection = _selectedServerIndex >= 0;
        _btnJoin.Enabled = hasSelection;
        _btnEdit.Enabled = hasSelection;
        _btnDelete.Enabled = hasSelection;
    }

    private void ConnectSelected()
    {
        if (_selectedServerIndex < 0) return;
        ServerData data = _serverList[_selectedServerIndex];
        ConnectToServer(data.Ip);
    }

    private void EditSelected()
    {
        if (_selectedServerIndex < 0) return;
        ServerData original = _serverList[_selectedServerIndex];
        ServerData temp = new(original.Name, original.Ip);
        Game.displayGuiScreen(new UIScreenAdapter(new EditServerScreen(Game, this, temp, true)));
    }

    public void ConfirmEdit(ServerData data, bool isEditing)
    {
        if (isEditing)
        {
            if (_selectedServerIndex >= 0)
            {
                _serverList[_selectedServerIndex].Name = data.Name;
                _serverList[_selectedServerIndex].Ip = data.Ip;
            }
        }
        else
        {
            _serverList.Add(data);
        }
        SaveServerList();
        PopulateServerList();
        UpdateButtons();
    }

    private void DeleteSelected()
    {
        if (_selectedServerIndex < 0) return;
        ServerData server = _serverList[_selectedServerIndex];
        string q = "Are you sure you want to remove this server?";
        string w = "'" + server.Name + "' " + "will be lost forever! (A long time!)";

        Game.displayGuiScreen(new UIScreenAdapter(new ConfirmationScreen(Game, this, q, w, "Delete", "Cancel", (result) =>
        {
            if (result)
            {
                _serverList.RemoveAt(_selectedServerIndex);
                SaveServerList();
                PopulateServerList();
                UpdateButtons();
            }
        })));
    }

    private void ConnectToServer(string ip)
    {
        string[] parts = ip.Split(':');
        string host = parts[0];
        int portNum = 25565;
        if (parts.Length > 1) int.TryParse(parts[1], out portNum);
        Game.displayGuiScreen(new GuiConnecting(Game, host, portNum));
    }
}
