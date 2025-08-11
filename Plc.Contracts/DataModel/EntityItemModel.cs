using Plc.Shared;

namespace Plc.Contracts.DataModel;

public struct EntityItemModel
{
    public string DBName;

    public S7DataTypeEnum S7DataType;

    public int DBAddress;

    public int OffSet;

    public byte? BitOffSet;

    public byte? ArrayLength { get; set; }

    public EntityItemModel(string dBName, S7DataTypeEnum s7DataType, int dBAddress, int offSet, byte? bitOffSet,
        byte? arrayLength)
    {
        DBName = dBName;
        S7DataType = s7DataType;
        DBAddress = dBAddress;
        OffSet = offSet;
        BitOffSet = bitOffSet;
        ArrayLength = arrayLength;
    }
}