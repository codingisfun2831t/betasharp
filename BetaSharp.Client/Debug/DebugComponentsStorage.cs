using BetaSharp.Client.Debug.Components;
using BetaSharp.NBT;
using Microsoft.Extensions.Logging;

namespace BetaSharp.Client.Debug;

public class DebugComponentsStorage
{
    private readonly ILogger<DebugComponentsStorage> _logger = Log.Instance.For<DebugComponentsStorage>();

    protected BetaSharp _game;
    private readonly string _componentsPath;

    public readonly DebugOverlay Overlay;

    public DebugComponentsStorage(BetaSharp game, string gameDataDir)
    {
        _game = game;
        _componentsPath = Path.Combine(gameDataDir, "components.dat");

        Overlay = new DebugOverlay(game);

        LoadComponents();
    }

    public static void DefaultComponents(List<DebugComponent> list)
    {
        void Right(DebugComponent comp)
        {
            comp.Right = true;
            list.Add(comp);
        }

        list.Add(new DebugVersion());
        list.Add(new DebugFPS());
        list.Add(new DebugEntities());
        list.Add(new DebugParticles());
        list.Add(new DebugWorld());
        list.Add(new DebugSeparator());
        list.Add(new DebugLocation());
        list.Add(new DebugSeparator());
        list.Add(new DebugServer());

        Right(new DebugFramework());
        Right(new DebugMemory());
        Right(new DebugSeparator());
        Right(new DebugSystem());
        Right(new DebugSeparator());
        Right(new DebugTargetedBlock());
    }

    public void LoadComponents()
    {
        try
        {
            if (!File.Exists(_componentsPath))
            {
                _logger.LogInformation("No components file found when loading, setting defaults and saving");
                DefaultComponents(Overlay.Components);
                SaveComponents();
                return;
            }

            using StreamReader reader = new(_componentsPath);

            NBTTagCompound tag = NbtIo.Read(reader.BaseStream);
            NBTTagList list = tag.GetTagList("Components");

            Overlay.Components.Clear();

            for (int i = 0; i < list.TagCount(); i++) {
                NBTBase nbt = list.TagAt(i);

                if (nbt is NBTTagCompound ntc)
                {
                    string type = ntc.GetString("_type");
                    DebugComponent? comp = DebugComponents.CreateFromTypeName(type);
                    if (comp is null)
                    {
                        _logger.LogWarning("Component " + i + " is a " + type + ", which couldnt be created!");
                    }

                    comp.Read(ntc);
                    Overlay.Components.Add(comp);
                }
            }
        }
        catch (Exception exception)
        {
            _logger.LogError($"Failed to load components: {exception.Message}");
        }
    }

    public void SaveComponents()
    {
        try
        {
            using var writer = new StreamWriter(_componentsPath);

            NBTTagCompound tag = new NBTTagCompound();
            NBTTagList list = new NBTTagList();

            tag.SetTag("Components", list);

            foreach (DebugComponent comp in Overlay.Components)
            {
                NBTTagCompound nbt = new NBTTagCompound();
                nbt.SetString("_type", comp.GetType().Name);
                comp.Write(nbt);
                list.SetTag(nbt);
            }
            NbtIo.Write(tag, writer.BaseStream);
        }
        catch (Exception exception)
        {
            _logger.LogError($"Failed to save components: {exception.Message}");
        }
    }
}
