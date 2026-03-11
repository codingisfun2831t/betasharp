using SixLabors.Fonts;
using TextRenderer = BetaSharp.Client.Rendering.TextRenderer;

namespace BetaSharp.Client.Guis.Debug;

public class DebugContext
{
    private bool _isRight;
    private int _leftY;
    private int _rightY;
    private BetaSharp _game;
    private int _scaledWidth;

    public DebugContext(BetaSharp game)
    {
        _game = game;
    }

    public void Inititalize()
    {
        ScaledResolution scaled = new(_game.options, _game.displayWidth, _game.displayHeight);
        _scaledWidth = scaled.ScaledWidth;

        _leftY = 2;
        _rightY = 2;
    }

    public void String(string str, Color? color = null)
    {
        if (!color.HasValue) color = Color.White;

        TextRenderer font = _game.fontRenderer;

        void leftString(string str, Color? clr = null)
        {
            font.DrawStringWrapped(str, 2, _leftY, _scaledWidth / 2, clr ?? Color.White);

            _leftY += font.GetStringHeight(str, _scaledWidth / 2);
        }

        void rightString(string str, Color? clr = null)
        {
            font.DrawStringWrapped(str, _scaledWidth / 2, _rightY, _scaledWidth / 2, clr ?? Color.White, HorizontalAlignment.Right);
            _rightY += font.GetStringHeight(str, _scaledWidth / 2);
        }

        if (_isRight) rightString(str, color);
        else leftString(str, color);
    }

    public void Seperator()
    {
        if (_isRight) _rightY += 10;
        else _leftY += 10;
    }

    public void DrawComponent(DebugComponent component)
    {
        _isRight = component.Right;
        component.Draw(this);
    }
}
