using System.Text;
using Common.Application.Log;
using Common.Helper;
using Common.Shared;
using Common.TransferBuffer;
using Plc.Application.Abstract;
using Plc.Contracts.Input;
using Plc.Contracts.Respon;
using Plc.Infrastructure.Helper;
using Plc.Shared;
using S7.Net;
using Serilog;

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
        UsePlc(netConfig.IsUse);
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
        _plc?.Close();
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
    public override Task<string> ReadTResultAsync(ReadBufferInput input)
    {
        var result = string.Empty;
        ;
        if (_plc.IsConnected == false)
        {
            Log.Logger.ForCategory(LogCategory.Net).Information($"{_plc.IP}--PLC未连接");
            return Task.FromResult(result);
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
                    var res = _plc.Read(dbType, input.DBAddress, input.DBStart, VarType.S7String,
                        input.ArratCount.Value);
                    result = res?.ToString();
                    break;
                }
                case S7DataTypeEnum.Array:
                {
                    var res = _plc.Read(dbType, input.DBAddress, input.DBStart, VarType.Byte, input.ArratCount.Value);
                    if (res is byte[] byteArray)
                    {
                        var hexValues = Array.ConvertAll(byteArray, b => b.ToString());
                        result = string.Join(Symbol.Split, hexValues);
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

        return Task.FromResult(result);
    }

    /// <summary>
    ///     数据写入
    /// </summary>
    /// <param name="bulkItems"></param>
    /// <exception cref="AggregateException"></exception>
    public override async Task WriteAsync(WriteBufferItemInput[] bulkItems)
    {
        if (!_plc.IsConnected)
            Log.Logger.ForCategory(LogCategory.Net).Information($"{_plc.IP}--PLC未连接");
        else
            try
            {
                for (var i = 0; i < bulkItems.Length; i++)
                {
                    var item = bulkItems[i];
                    var dbType = EnumConvert.S7BlockTypeToDataType(item.S7BlockType);
                    if (item.Buffer == null)
                        switch (item.S7DataType)
                        {
                            case S7DataTypeEnum.Bool:
                            {
                                bool.TryParse(item.Value, out var @bool);
                                _plc.WriteBit(dbType, item.DBAddress, item.DBStart, item.DBBit.Value, @bool);
                                break;
                            }
                            case S7DataTypeEnum.Byte:
                            {
                                byte.TryParse(item.Value, out var @byte);
                                item.Buffer = new[] { @byte };
                                await _plc.WriteBytesAsync(dbType, item.DBAddress, item.DBStart, item.Buffer);
                                break;
                            }
                            case S7DataTypeEnum.Int:
                            {
                                short.TryParse(item.Value, out var @short);
                                item.Buffer = TransferBufferHelper.IntToByteArray(@short);
                                await _plc.WriteBytesAsync(dbType, item.DBAddress, item.DBStart, item.Buffer);
                                break;
                            }
                            case S7DataTypeEnum.DInt:
                            {
                                int.TryParse(item.Value, out var result);
                                item.Buffer = TransferBufferHelper.DIntToByteArray(result);
                                await _plc.WriteBytesAsync(dbType, item.DBAddress, item.DBStart, item.Buffer);
                                break;
                            }
                            case S7DataTypeEnum.Word:
                            {
                                ushort.TryParse(item.Value, out var result);
                                item.Buffer = TransferBufferHelper.WordToByteArray(result);
                                await _plc.WriteBytesAsync(dbType, item.DBAddress, item.DBStart, item.Buffer);
                                break;
                            }
                            case S7DataTypeEnum.DWord:
                            {
                                uint.TryParse(item.Value, out var result);
                                item.Buffer = TransferBufferHelper.DWordToByteArray(result);
                                await _plc.WriteBytesAsync(dbType, item.DBAddress, item.DBStart, item.Buffer);
                                break;
                            }
                            case S7DataTypeEnum.Real:
                            {
                                float.TryParse(item.Value, out var result);
                                item.Buffer = TransferBufferHelper.RealToByteArray(result);
                                await _plc.WriteBytesAsync(dbType, item.DBAddress, item.DBStart, item.Buffer);
                                break;
                            }
                            case S7DataTypeEnum.LReal:
                            {
                                double.TryParse(item.Value, out var result);
                                item.Buffer = TransferBufferHelper.LRealToByteArray(result);
                                await _plc.WriteBytesAsync(dbType, item.DBAddress, item.DBStart, item.Buffer);
                                break;
                            }
                            case S7DataTypeEnum.String:
                            {
                                item.Buffer = TransferBufferHelper.StringToByteArray(item.Value, item.ArratCount.Value);
                                await _plc.WriteBytesAsync(dbType, item.DBAddress, item.DBStart, item.Buffer);
                                break;
                            }
                            case S7DataTypeEnum.S7String:
                            {
                                item.Buffer =
                                    TransferBufferHelper.S7StringToByteArray(item.Value, item.ArratCount.Value,
                                        Encoding.ASCII);
                                await _plc.WriteBytesAsync(dbType, item.DBAddress, item.DBStart, item.Buffer);
                                break;
                            }
                            case S7DataTypeEnum.Array:
                            {
                                await _plc.WriteBytesAsync(dbType, item.DBAddress, item.DBStart, item.Buffer);
                                break;
                            }
                            default:
                                Log.Logger.ForCategory(LogCategory.Net).Information($"{_plc.IP}--PLC无解析数据");
                                break;
                        }
                }
            }
            catch (Exception ex)
            {
                Log.Logger.ForCategory(LogCategory.Net).Information($"数据读取失败: {_plc.IP}-{ex}");
            }
    }


    public override async Task<bool> CheckWriteToBytesAsync(WriteBufferItemInput[] bulkItems)
    {
        var result = true;
        if (!_plc.IsConnected)
        {
            Log.Logger.ForCategory(LogCategory.Net).Information($"{_plc.IP}--PLC未连接");
            result = false;
        }
        else
        {
            for (var i = 0; i < bulkItems.Length; i++)
            {
                var item = bulkItems[i];
                var dbType = EnumConvert.S7BlockTypeToDataType(item.S7BlockType);
                switch (item.S7DataType)
                {
                    case S7DataTypeEnum.Bool:
                    {
                        bool.TryParse(item.Value, out var @bool);
                        _plc.WriteBit(dbType, item.DBAddress, item.DBStart, item.DBBit.Value, @bool);
                        var res = _plc.Read(dbType, item.DBAddress, item.DBStart, VarType.Bit, item.DBBit.Value);
                        if (!@bool.Equals(res))
                            result = false;
                        break;
                    }
                    case S7DataTypeEnum.Byte:
                    {
                        byte.TryParse(item.Value, out var @byte);
                        item.Buffer = new[] { @byte };
                        await _plc.WriteBytesAsync(dbType, item.DBAddress, item.DBStart, item.Buffer);
                        var buffer = _plc.ReadBytes(dbType, item.DBAddress, item.DBStart, 1);
                        if (SequenceEquals.ByteSequenceEquals(buffer, item.Buffer) == false) result = false;
                        break;
                    }
                    case S7DataTypeEnum.Int:
                    {
                        short.TryParse(item.Value, out var @short);
                        item.Buffer = TransferBufferHelper.IntToByteArray(@short);
                        await _plc.WriteBytesAsync(dbType, item.DBAddress, item.DBStart, item.Buffer);
                        var buffer = _plc.ReadBytes(dbType, item.DBAddress, item.DBStart, 2);
                        if (SequenceEquals.ByteSequenceEquals(buffer, item.Buffer) == false) result = false;
                        break;
                    }
                    case S7DataTypeEnum.DInt:
                    {
                        int.TryParse(item.Value, out var @int);
                        item.Buffer = TransferBufferHelper.DIntToByteArray(@int);
                        await _plc.WriteBytesAsync(dbType, item.DBAddress, item.DBStart, item.Buffer);
                        var buffer = _plc.ReadBytes(dbType, item.DBAddress, item.DBStart, 4);
                        if (SequenceEquals.ByteSequenceEquals(buffer, item.Buffer) == false) result = false;
                        break;
                    }
                    case S7DataTypeEnum.Word:
                    {
                        ushort.TryParse(item.Value, out var @ushort);
                        item.Buffer = TransferBufferHelper.WordToByteArray(@ushort);
                        await _plc.WriteBytesAsync(dbType, item.DBAddress, item.DBStart, item.Buffer);
                        var buffer = _plc.ReadBytes(dbType, item.DBAddress, item.DBStart, 2);
                        if (SequenceEquals.ByteSequenceEquals(buffer, item.Buffer) == false) result = false;
                        break;
                    }
                    case S7DataTypeEnum.DWord:
                    {
                        uint.TryParse(item.Value, out var @uint);
                        item.Buffer = TransferBufferHelper.DWordToByteArray(@uint);
                        await _plc.WriteBytesAsync(dbType, item.DBAddress, item.DBStart, item.Buffer);
                        var buffer = _plc.ReadBytes(dbType, item.DBAddress, item.DBStart, 4);
                        if (SequenceEquals.ByteSequenceEquals(buffer, item.Buffer) == false) result = false;
                        break;
                    }
                    case S7DataTypeEnum.Real:
                    {
                        float.TryParse(item.Value, out var @float);
                        item.Buffer = TransferBufferHelper.RealToByteArray(@float);
                        await _plc.WriteBytesAsync(dbType, item.DBAddress, item.DBStart, item.Buffer);
                        var buffer = _plc.ReadBytes(dbType, item.DBAddress, item.DBStart, 4);
                        if (SequenceEquals.ByteSequenceEquals(buffer, item.Buffer) == false) result = false;
                        break;
                    }
                    case S7DataTypeEnum.LReal:
                    {
                        double.TryParse(item.Value, out var @double);
                        item.Buffer = TransferBufferHelper.LRealToByteArray(@double);
                        await _plc.WriteBytesAsync(dbType, item.DBAddress, item.DBStart, item.Buffer);
                        var buffer = _plc.ReadBytes(dbType, item.DBAddress, item.DBStart, 8);
                        if (SequenceEquals.ByteSequenceEquals(buffer, item.Buffer) == false) result = false;
                        break;
                    }
                    case S7DataTypeEnum.String:
                    {
                        item.Buffer = TransferBufferHelper.StringToByteArray(item.Value, item.ArratCount.Value);
                        await _plc.WriteBytesAsync(dbType, item.DBAddress, item.DBStart, item.Buffer);
                        var buffer = _plc.ReadBytes(dbType, item.DBAddress, item.DBStart, item.ArratCount.Value);
                        if (SequenceEquals.ByteSequenceEquals(buffer, item.Buffer) == false) result = false;
                        break;
                    }
                    case S7DataTypeEnum.S7String:
                    {
                        item.Buffer =
                            TransferBufferHelper.S7StringToByteArray(item.Value, item.ArratCount.Value,
                                Encoding.ASCII);
                        await _plc.WriteBytesAsync(dbType, item.DBAddress, item.DBStart, item.Buffer);
                        var buffer = _plc.ReadBytes(dbType, item.DBAddress, item.DBStart, item.ArratCount.Value);
                        if (SequenceEquals.ByteSequenceEquals(buffer, item.Buffer) == false) result = false;
                        break;
                    }
                    case S7DataTypeEnum.Array:
                    {
                        await _plc.WriteBytesAsync(dbType, item.DBAddress, item.DBStart, item.Buffer);
                        var buffer = _plc.ReadBytes(dbType, item.DBAddress, item.DBStart, item.Buffer.Length);
                        if (SequenceEquals.ByteSequenceEquals(buffer, item.Buffer) == false) result = false;
                        break;
                    }
                    default:
                        Log.Logger.ForCategory(LogCategory.Net).Information($"{_plc.IP}--PLC无解析数据");
                        break;
                }

                if (result == false)
                    break;
            }
        }

        return result;
    }
}