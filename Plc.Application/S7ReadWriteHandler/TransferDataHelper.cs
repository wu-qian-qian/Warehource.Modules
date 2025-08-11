using System.Text;
using Common.Application.Log;
using Common.Shared;
using Common.TransferBuffer;
using Plc.Shared;
using Serilog;

namespace Plc.Application.S7ReadWriteHandler;

public class TransferDataHelper
{
    public static string TransferBufferToData(byte[] buffer, int offset, byte bitOffset, S7DataTypeEnum s7DataType,
        byte? array)
    {
        var result = string.Empty;
        switch (s7DataType)
        {
            case S7DataTypeEnum.Bool:
            {
                var res = buffer.Skip(offset).Take(1).ToArray();
                result = TransferBufferHelper.ByteFromBool(res[0], bitOffset).ToString();
                break;
            }
            case S7DataTypeEnum.Byte:
            {
                var res = buffer.Skip(offset).Take(1).ToArray();
                result = res[0].ToString();
                break;
            }
            case S7DataTypeEnum.Int:
            {
                var res = buffer.Skip(offset).Take(2).ToArray();
                result = TransferBufferHelper.IntFromByteArray(res).ToString();
                break;
            }
            case S7DataTypeEnum.DInt:
            {
                var res = buffer.Skip(offset).Take(4).ToArray();
                result = TransferBufferHelper.DIntFromByteArray(res).ToString();
                break;
            }
            case S7DataTypeEnum.Word:
            {
                var res = buffer.Skip(offset).Take(2).ToArray();
                result = TransferBufferHelper.WordFromByteArray(res).ToString();
                break;
            }
            case S7DataTypeEnum.DWord:
            {
                var res = buffer.Skip(offset).Take(4).ToArray();
                result = TransferBufferHelper.DWordFromByteArray(res).ToString();
                break;
            }
            case S7DataTypeEnum.Real:
            {
                var res = buffer.Skip(offset).Take(4).ToArray();
                result = TransferBufferHelper.RealFromByteArray(res).ToString();
                break;
            }
            case S7DataTypeEnum.LReal:
            {
                var res = buffer.Skip(offset).Take(8).ToArray();
                result = TransferBufferHelper.LRealFromByteArray(res).ToString();
                break;
            }
            case S7DataTypeEnum.String:
            {
                var res = buffer.Skip(offset).Take(array.Value).ToArray();
                result = TransferBufferHelper.S7StringFromByteArray(res, Encoding.ASCII);
                break;
            }
            case S7DataTypeEnum.S7String:
            {
                var res = buffer.Skip(offset).Take(array.Value).ToArray();
                result = TransferBufferHelper.S7StringFromByteArray(res, Encoding.ASCII);
                break;
            }
            case S7DataTypeEnum.Array:
            {
                var res = buffer.Skip(offset).Take(array.Value).ToArray();
                var hexValues = Array.ConvertAll(res, b => b.ToString());
                result = string.Join(Symbol.Split, hexValues);
                break;
            }
            default:
                Log.Logger.ForCategory(LogCategory.Business).Information($"{s7DataType}--PLC无解析数据");
                break;
        }

        return result;
    }
}