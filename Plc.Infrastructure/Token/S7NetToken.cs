using System.Text;
using Common.Application.Log;
using Common.Shared;
using Common.TransferBuffer;
using Plc.Application.Abstract;
using Plc.Contracts.Input;
using Plc.Contracts.Respon;
using Plc.Infrastructure.Helper;
using Plc.Shared;
using S7.Net;
using Serilog;
using String = System.String;

namespace Plc.Infrastructure.Token;

public partial class S7NetToken : S7Net
{
    public S7NetToken(S7NetDto netConfig)
    {
        var s7Cpu = netConfig.S7Type switch
        {
            S7TypeEnum.S71200 => CpuType.S71200,
            S7TypeEnum.S71500 => CpuType.S71500,
            _ => throw new ArgumentException("无可用类型")
        };
        _plc = new S7.Net.Plc(s7Cpu, netConfig.Ip, netConfig.Port, netConfig.Rack, netConfig.Solt);
        _plc.ReadTimeout = netConfig.ReadTimeOut;
        _plc.WriteTimeout = netConfig.WriteTimeOut;
        if (netConfig.IsUse)
        {
           this.Connect();
        }
    }

    public override void Connect()
    {
        try
        {
            _plc.Open();
            Log.Logger.ForCategory(LogCategory.Net).Information($"{_plc.IP}--链接成功");
        }
        catch (PlcException ex)
        {
            Log.Logger.ForCategory(LogCategory.Net).Information($"{_plc.IP}--链接失败，{ex.Message}");
        }
    }

    public override void ReConnect()
    {
        Log.Logger.ForCategory(LogCategory.Net).Information($"{_plc.IP}--开始重新链接");
        Connect();
    }

    public override void Close()
    {
        _plc.Close();
        Log.Logger.ForCategory(LogCategory.Net).Information($"{_plc.IP}--关闭链接");
    }

    /// <summary>
    ///     需要反转
    ///     批量读取
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    public override Task<byte[]> ReadAsync(ReadBufferInput input)
    {
        var bulkItem = CreatReadS7Item(input);
        byte[]? bufferBlock = default;
        if (_plc.IsConnected)
            try
            {
                var result = _plc.ReadBytes(DataType.DataBlock, bulkItem.DB, bulkItem.StartByteAdr, bulkItem.Count);
                bufferBlock = result ?? Array.Empty<byte>();
                Log.Logger.ForCategory(LogCategory.Net).Information($"{_plc.IP}--读取数据成功");
            }
            catch (Exception e)
            {
                Log.Logger.ForCategory(LogCategory.Net).Information($"读取失败详细{e.Message}");
            }
        else
            Log.Logger.ForCategory(LogCategory.Net).Information($"{_plc.IP}--PLC未连接");

        return Task.FromResult(bufferBlock);
    }

    /// <summary>
    ///     直接读取并转换类型
    /// </summary>
    /// <param name="input"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public override async Task<string> ReadTResultAsync(ReadBufferInput input)
    {
        string result=String.Empty;;
        if (_plc.IsConnected == false)
        {
            Log.Logger.ForCategory(LogCategory.Net).Information($"{_plc.IP}--PLC未连接");
            return result;
        }

        try
        {
            var dbType = EnumConvert.S7BlockTypeToDataType(input.S7BlockType);
            switch (input.S7DataType)
            {
                case S7DataTypeEnum.Bool:
                {
                    var res = _plc.Read(dbType, input.DBAddress, input.DBStart, VarType.Bit, input.DBBit.Value);
                    result = res?.ToString();
                    break;
                }
                case S7DataTypeEnum.Byte:
                {
                    var res = _plc.Read(dbType, input.DBAddress, input.DBStart, VarType.Byte, 1);
                    result = res?.ToString();
                    break;
                }
                case S7DataTypeEnum.Int:
                {
                    var res = _plc.Read(dbType, input.DBAddress, input.DBStart, VarType.Int, 1);
                    result = res?.ToString();
                    break;
                }
                case S7DataTypeEnum.DInt:
                {
                    var res = _plc.Read(dbType, input.DBAddress, input.DBStart, VarType.DInt, 1);
                    result = res?.ToString();
                    break;
                }
                case S7DataTypeEnum.Word:
                {
                    var res = _plc.Read(dbType, input.DBAddress, input.DBStart, VarType.Word, 1);
                    result = res?.ToString();
                    break;
                }
                case S7DataTypeEnum.DWord:
                {
                    var res = _plc.Read(dbType, input.DBAddress, input.DBStart, VarType.DWord, 1);
                    result = res?.ToString();
                    break;
                }
                case S7DataTypeEnum.Real:
                {
                    var res = _plc.Read(dbType, input.DBAddress, input.DBStart, VarType.Real, 1);
                    result = res?.ToString();
                    break;
                }
                case S7DataTypeEnum.LReal:
                {
                    var res = _plc.Read(dbType, input.DBAddress, input.DBStart, VarType.LReal, 1);
                    result = res?.ToString();
                    break;
                }
                case S7DataTypeEnum.String:
                {
                    var res = _plc.Read(dbType, input.DBAddress, input.DBStart, VarType.String, input.ArratCount.Value);
                    result = res?.ToString();
                    break;
                }
                case S7DataTypeEnum.S7String:
                {
                    var res = _plc.Read(dbType, input.DBAddress, input.DBStart, VarType.S7String, input.ArratCount.Value);
                    result = res?.ToString();
                    break;
                }
                case S7DataTypeEnum.Array:
                {
                    string separator = "-";
                    var res = _plc.Read(dbType, input.DBAddress, input.DBStart, VarType.Byte, input.ArratCount.Value);
                    if (res is byte[] byteArray)
                    {
                        string[] hexValues = Array.ConvertAll(byteArray, b => b.ToString());
                        result= string.Join(separator, hexValues);
                    }
                    break;
                }
                default:
                    Log.Logger.ForCategory(LogCategory.Net).Information($"{_plc.IP}--PLC无解析数据");
                    break;
            }
        }
        catch (Exception e)
        {
            Log.Logger.ForCategory(LogCategory.Net).Information($"{_plc.IP}--PLC读取出现异常：{e.Message}");
        }
        return result;
    }

    /// <summary>
    ///     数据写入
    /// </summary>
    /// <param name="bulkItems"></param>
    /// <exception cref="AggregateException"></exception>
    public override async Task WriteAsync(WriteBufferItemInput[] bulkItems)
    {
        if (!_plc.IsConnected)
        {
            Log.Logger.ForCategory(LogCategory.Net).Information($"{_plc.IP}--PLC未连接");
        }
        else
        {
            foreach (var item in bulkItems)
            {
                 if (item.Buffer == null)
                {
                      switch (item.S7DataType)
                    {
                        case S7DataTypeEnum.Bool:
                        {
                            var dbType = EnumConvert.S7BlockTypeToDataType(item.S7BlockType);
                            bool.TryParse(item.Value, out var @bool);
                            _plc.WriteBit(dbType, item.DBAddress, item.DBStart, item.DBBit.Value, @bool);
                            break;
                        }
                        case S7DataTypeEnum.Byte:
                        {
                            byte.TryParse(item.Value, out var @byte);
                            item.Buffer = new[] { @byte };
                            break;
                        }
                        case S7DataTypeEnum.Int:
                        {
                            short.TryParse(item.Value, out var @short);
                            item.Buffer = TransferBufferHelper.IntToByteArray(@short);
                            break;
                        }
                        case S7DataTypeEnum.DInt:
                        {
                            int.TryParse(item.Value, out var result);
                            item.Buffer = TransferBufferHelper.DIntToByteArray(result);
                            break;
                        }
                        case S7DataTypeEnum.Word:
                        {
                            ushort.TryParse(item.Value, out var result);
                            item.Buffer = TransferBufferHelper.WordToByteArray(result);
                            break;
                        }
                        case S7DataTypeEnum.DWord:
                        {
                            uint.TryParse(item.Value, out var result);
                            item.Buffer = TransferBufferHelper.DWordToByteArray(result);
                            break;
                        }
                        case S7DataTypeEnum.Real:
                        {
                            float.TryParse(item.Value, out var result);
                            item.Buffer = TransferBufferHelper.RealToByteArray(result);
                            break;
                        }
                        case S7DataTypeEnum.LReal:
                        {
                            double.TryParse(item.Value, out var result);
                            item.Buffer = TransferBufferHelper.LRealToByteArray(result);
                            break;
                        }
                        case S7DataTypeEnum.String:
                        {
                            item.Buffer = TransferBufferHelper.StringToByteArray(item.Value, item.ArratCount.Value);
                            break;
                        }
                        case S7DataTypeEnum.S7String:
                        {
                            item.Buffer =
                                TransferBufferHelper.S7StringToByteArray(item.Value, item.ArratCount.Value, Encoding.ASCII);
                            break;
                        }
                        default:
                            Log.Logger.ForCategory(LogCategory.Net).Information($"{_plc.IP}--PLC无解析数据");
                            break;
                    }
                }
            }
            try
            {
                await WriteToBytesAsync(bulkItems);
                Log.Logger.ForCategory(LogCategory.Net).Information($"{_plc.IP}--写入数据成功");
            }
            catch (Exception e)
            {
                Log.Logger.ForCategory(LogCategory.Net).Information($"写入失败详细{e.Message}");
            }
        }
    }
    
    public override async Task WriteToBytesAsync(WriteBufferItemInput[] bulkItems)
    {
        if (!_plc.IsConnected)
            Log.Logger.ForCategory(LogCategory.Net).Information($"{_plc.IP}--PLC未连接");
        else
            foreach (var item in bulkItems)
            {
                if(item.S7DataType==S7DataTypeEnum.Bool)
                    continue;
                var dbType = EnumConvert.S7BlockTypeToDataType(item.S7BlockType);
                await _plc.WriteBytesAsync(dbType, item.DBAddress, item.DBStart, item.Buffer);
            }
    }

    public  override async Task<bool> CheckWriteToBytesAsync(WriteBufferItemInput[] bulkItems)
    {
        bool @bool = true;
        if (!_plc.IsConnected)
        {
            Log.Logger.ForCategory(LogCategory.Net).Information($"{_plc.IP}--PLC未连接");
            @bool = false;
        }
        else
        {
            foreach (var item in bulkItems)
            {
                if (item.Buffer != null)
                {
                    var dbType = EnumConvert.S7BlockTypeToDataType(item.S7BlockType);
                    await _plc.WriteBytesAsync(dbType, item.DBAddress, item.DBStart, item.Buffer);
                    var buffer=await  ReadAsync(new ReadBufferInput
                    {
                        DBBit = item.DBBit,
                        DBAddress = item.DBAddress,
                        S7BlockType = item.S7BlockType,
                        DBEnd = item.Buffer.Length,
                        DBStart = item.DBStart,
                    });
                    if (ReferenceEquals(item.Buffer, buffer) == false)
                    {
                        @bool = false;
                        break;
                    }
                }
            }
        }
        return @bool;
    }
}