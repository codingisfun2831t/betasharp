using BetaSharp.Client.Options;

namespace BetaSharp.Client.UI.Screens;

public class VideoSettingsScreen(UIScreen? parent, GameOptions options) : BaseOptionsScreen(parent, options, "options.videoTitle")
{
    protected override IEnumerable<GameOption> GetOptions() => Options.VideoScreenOptions;
}
