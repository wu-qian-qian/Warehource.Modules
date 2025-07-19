namespace Wcs.Shared;

[AttributeUsage(AttributeTargets.All, Inherited = false)]
public class S7DataTypeAttribute : Attribute
{
    public S7DataTypeAttribute(byte dataSize)
    {
        DataSize = dataSize;
    }

    public byte DataSize { get; set; }
}