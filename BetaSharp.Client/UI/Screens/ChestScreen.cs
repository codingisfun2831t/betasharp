using BetaSharp.Client.UI.Controls;
using BetaSharp.Client.UI.Rendering;
using BetaSharp.Inventorys;
using BetaSharp.Client.UI.Layout.Flexbox;
using BetaSharp.Screens;
using BetaSharp.Client.Guis;

namespace BetaSharp.Client.UI.Screens;

public class ChestScreen : ContainerScreen
{
    private readonly IInventory _upperInventory;
    private readonly IInventory _lowerInventory;
    private readonly int _inventoryRows;

    public ChestScreen(IInventory upperInventory, IInventory lowerInventory) 
        : base(new GenericContainerScreenHandler(upperInventory, lowerInventory))
    {
        _upperInventory = upperInventory;
        _lowerInventory = lowerInventory;
        _inventoryRows = lowerInventory.size() / 9;
        _ySize = 114 + _inventoryRows * 18;
    }

    protected override void Init()
    {
        base.Init();

        // Background Image
        var background = new Image 
        { 
            Texture = Renderer.TextureManager.GetTextureId("/gui/container.png"),
            U = 0,
            V = 0,
            UWidth = 176,
            VHeight = _ySize
        };
        background.Style.Width = _xSize;
        background.Style.Height = _ySize;
        background.Style.Position = PositionType.Absolute;
        _containerPanel.AddChild(background);

        // Labels
        var lblUpper = new Label { Text = _lowerInventory.getName(), HasShadow = false };
        lblUpper.TextColor = Color.Gray40;
        lblUpper.Style.Position = PositionType.Absolute;
        lblUpper.Style.Left = 8;
        lblUpper.Style.Top = 6;
        _containerPanel.AddChild(lblUpper);

        var lblLower = new Label { Text = _upperInventory.getName(), HasShadow = false };
        lblLower.TextColor = Color.Gray40;
        lblLower.Style.Position = PositionType.Absolute;
        lblLower.Style.Left = 8;
        lblLower.Style.Top = _ySize - 96 + 2;
        _containerPanel.AddChild(lblLower);

        AddSlots();
    }
}
