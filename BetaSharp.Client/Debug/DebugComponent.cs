using System.ComponentModel;
using BetaSharp.NBT;

namespace BetaSharp.Client.Debug;

public abstract class DebugComponent
{
    [Description("Whether to show this component on the right side of the debug overlay instead of the left.")]
    public bool Right { get; set; }
    public abstract IEnumerable<DebugRowData> GetRows(DebugContext ctx);
    public abstract DebugComponent Duplicate();

    public void Write(NBTTagCompound nbt)
    {
        nbt.SetBoolean("Right", Right);

        WriteNBT(nbt);
    }

    public void Read(NBTTagCompound nbt)
    {
        Right = nbt.GetBoolean("Right");

        ReadNBT(nbt);
    }

    public virtual void WriteNBT(NBTTagCompound nbt) { }
    public virtual void ReadNBT(NBTTagCompound nbt) { }
}
