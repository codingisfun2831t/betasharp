using BetaSharp.NBT;

namespace BetaSharp.Client.Guis.Debug;

public abstract class DebugComponent
{
    public bool Right { get; set; }
    public abstract void Draw(DebugContext context);
    public abstract DebugComponent Duplicate();

    public void write(NBTTagCompound nbt)
    {
        nbt.SetBoolean("Right", Right);

        writeNBT(nbt);
    }

    public void read(NBTTagCompound nbt)
    {
        Right = nbt.GetBoolean("Right");

        readNBT(nbt);
    }

    public virtual void writeNBT(NBTTagCompound nbt) { }
    public virtual void readNBT(NBTTagCompound nbt) { }
}
