using System;
using System.Collections.Generic;
using System.Text;

namespace BetaSharp.Client.Options;

public class ParityOptions
{
    public Dictionary<string, GameOption> Options { get; set; } = new Dictionary<string, GameOption>();
    public Dictionary<string, string> OptionDescriptions { get; set; } = new Dictionary<string, string>();

    public ParityOptions()
    {
        AddBoolOption("torchPositioning", "Torch Position",
            "If ON, fix torch positioning (disallow placing straight up floating, without leaning to a block), otherwise emulate vanilla behavior.",
            true);
    }

    private void AddBoolOption(string id, string label, string description, bool def)
    {
        Options.Add(id, new BoolOption(label, "parity." + id, def));
        OptionDescriptions.Add(id, description);
    }
}
