using BetaSharp.Client.UI.Controls;
using BetaSharp.Client.UI.Rendering;
using BetaSharp.Entities;
using BetaSharp.Client.UI.Layout.Flexbox;
using BetaSharp.Client.Guis;

namespace BetaSharp.Client.UI.Screens;

public class InventoryScreen : ContainerScreen
{
    private EntityPreview _playerPreview = null!;

    public InventoryScreen(EntityPlayer player) : base(player.playerScreenHandler)
    {
        player.increaseStat(global::BetaSharp.Achievements.OpenInventory, 1);
    }

    protected override void Init()
    {
        base.Init();

        // Background Image
        var background = new Image 
        { 
            Texture = Renderer.TextureManager.GetTextureId("/gui/inventory.png"),
            U = 0,
            V = 0,
            UWidth = 176,
            VHeight = 166
        };
        background.Style.Width = _xSize;
        background.Style.Height = _ySize;
        background.Style.Position = PositionType.Absolute;
        _containerPanel.AddChild(background);

        // Player Preview
        _playerPreview = new EntityPreview();
        _playerPreview.Entity = Game.player;
        _playerPreview.Scale = 30.0f;
        _playerPreview.Style.Position = PositionType.Absolute;
        _playerPreview.Style.Left = 51;
        _playerPreview.Style.Top = 75 - 50; // Adjustment from legacy logic
        _playerPreview.Style.Width = 30; // Rough size for centering
        _playerPreview.Style.Height = 50;
        _containerPanel.AddChild(_playerPreview);

        // Labels
        var lblCrafting = new Label { Text = "Crafting", HasShadow = false };
        lblCrafting.TextColor = Color.Gray40;
        lblCrafting.Style.Position = PositionType.Absolute;
        lblCrafting.Style.Left = 86;
        lblCrafting.Style.Top = 16;
        _containerPanel.AddChild(lblCrafting);

        // Slots should be added last so they are on top
        AddSlots();
    }
}
