using BetaSharp.Blocks.Entities;
using BetaSharp.Client.UI.Controls;
using BetaSharp.Client.UI.Rendering;
using BetaSharp.Inventorys;
using BetaSharp.Client.UI.Layout.Flexbox;
using BetaSharp.Screens;
using BetaSharp.Client.Guis;

namespace BetaSharp.Client.UI.Screens;

public class FurnaceScreen : ContainerScreen
{
    private readonly BlockEntityFurnace _furnace;

    public FurnaceScreen(InventoryPlayer playerInventory, BlockEntityFurnace furnace) 
        : base(new FurnaceScreenHandler(playerInventory, furnace))
    {
        _furnace = furnace;
    }

    protected override void Init()
    {
        base.Init();

        // Background Image
        var background = new Image 
        { 
            Texture = Renderer.TextureManager.GetTextureId("/gui/furnace.png"),
            U = 0,
            V = 0,
            UWidth = 176,
            VHeight = 166
        };
        background.Style.Width = _xSize;
        background.Style.Height = _ySize;
        background.Style.Position = PositionType.Absolute;
        _containerPanel.AddChild(background);

        // Labels
        var lblFurnace = new Label { Text = "Furnace", HasShadow = false };
        lblFurnace.TextColor = Color.Gray40;
        lblFurnace.Style.Position = PositionType.Absolute;
        lblFurnace.Style.Left = 60;
        lblFurnace.Style.Top = 6;
        _containerPanel.AddChild(lblFurnace);

        var lblInventory = new Label { Text = "Inventory", HasShadow = false };
        lblInventory.TextColor = Color.Gray40;
        lblInventory.Style.Position = PositionType.Absolute;
        lblInventory.Style.Left = 8;
        lblInventory.Style.Top = _ySize - 96 + 2;
        _containerPanel.AddChild(lblInventory);

        // Progress Indicators
        // Burning Fire
        var fireProgress = new FurnaceFireProgress(_furnace);
        fireProgress.Style.Position = PositionType.Absolute;
        fireProgress.Style.Left = 56;
        fireProgress.Style.Top = 36;
        _containerPanel.AddChild(fireProgress);

        // Smelting Arrow
        var smeltProgress = new FurnaceSmeltProgress(_furnace);
        smeltProgress.Style.Position = PositionType.Absolute;
        smeltProgress.Style.Left = 79;
        smeltProgress.Style.Top = 34;
        _containerPanel.AddChild(smeltProgress);

        AddSlots();
    }
}

// Custom controls for Furnace progress to keep rendering out of the screen
public class FurnaceFireProgress(BlockEntityFurnace furnace) : UIElement
{
    public override void Render(UIRenderer renderer)
    {
        if (furnace.isBurning())
        {
            int progress = furnace.getFuelTimeDelta(12);
            var texture = renderer.TextureManager.GetTextureId("/gui/furnace.png");
            renderer.DrawTexturedModalRect(texture, 0, 12 - progress, 176, 12 - progress, 14, progress + 2);
        }
        base.Render(renderer);
    }
}

public class FurnaceSmeltProgress(BlockEntityFurnace furnace) : UIElement
{
    public override void Render(UIRenderer renderer)
    {
        int progress = furnace.getCookTimeDelta(24);
        var texture = renderer.TextureManager.GetTextureId("/gui/furnace.png");
        renderer.DrawTexturedModalRect(texture, 0, 0, 176, 14, progress + 1, 16);
        base.Render(renderer);
    }
}
