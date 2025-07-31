using Common.Application.Log;
using Common.Helper;
using Common.Shared;
using Plc.Contracts.Input;
using Plc.Contracts.Respon;
using Plc.Infrastructure.Helper;
using Plc.Shared;
using S7.Net;
using S7.Net.Types;
using Serilog;

namespace Plc.Infrastructure.S7Net;

public class S7NetToken : Application.Net.S7Net
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
    /// 需要反转
    /// 批量读取
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    public override Task<byte[]> ReadAsync(ReadBufferInput input)
    {
       var bulkItem = CreatReadS7Item(input);
        byte[] bufferBlock = default;
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
    /// 直接读取并转换类型
    /// </summary>
    /// <param name="input"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public override async Task<T> ReadTResultAsync<T>(ReadBufferInput input)
    {
        T t = default(T);
        if (_plc.IsConnected == false)
        {
            Log.Logger.ForCategory(LogCategory.Net).Information($"{_plc.IP}--PLC未连接");
            return t;
        }
        else
        {
            try
            {
                var dbType = EnumConvert.S7BlockTypeToDataType(input.S7BlockType);
                switch (input.S7DataType)
                {
                    case S7DataTypeEnum.Bool:
                    {
                        var res = _plc.Read(dbType, input.DBAddress, input.DBStart, VarType.Bit, input.DBBit);
                        t = (T)res;
                        break;
                    }
                    case S7DataTypeEnum.Byte:
                    {
                        var res = _plc.Read(dbType, input.DBAddress, input.DBStart, VarType.Byte,1);
                        t = (T)res;
                        break;
                    }
                    case S7DataTypeEnum.Int:
                    {
                        var res = _plc.Read(dbType, input.DBAddress, input.DBStart, VarType.Int,1);
                        t = (T)res;
                        break;
                    }
                    case S7DataTypeEnum.DInt: 
                    {
                        var res = _plc.Read(dbType, input.DBAddress, input.DBStart, VarType.DInt,1);
                        t = (T)res;
                        break;
                    }
                    case S7DataTypeEnum.Word: 
                    {
                        var res = _plc.Read(dbType, input.DBAddress, input.DBStart, VarType.Word,1);
                        t = (T)res;
                        break;
                    }
                    case S7DataTypeEnum.DWord: 
                    {
                        var res = _plc.Read(dbType, input.DBAddress, input.DBStart, VarType.DWord,1);
                        t = (T)res;
                        break;
                    }
                    case S7DataTypeEnum.Real: 
                    {
                        var res = _plc.Read(dbType, input.DBAddress, input.DBStart, VarType.Real,1);
                        t = (T)res;
                        break;
                    }
                    case S7DataTypeEnum.LReal:    
                    {
                        var res = _plc.Read(dbType, input.DBAddress, input.DBStart, VarType.LReal,1);
                        t = (T)res;
                        break;
                    }
                    case S7DataTypeEnum.String:
                    {
                        var res = _plc.Read(dbType, input.DBAddress, input.DBStart, VarType.String,input.ArratCount);
                        t = (T)res;
                        break;
                    }
                    case S7DataTypeEnum.S7String:
                    {
                        var res = _plc.Read(dbType, input.DBAddress, input.DBStart, VarType.S7String,input.ArratCount);
                        t = (T)res;
                        break;
                    }
                    case S7DataTypeEnum.Array:
                    {
                        var res = _plc.Read(dbType, input.DBAddress, input.DBStart, VarType.Byte,input.ArratCount);
                        t = (T)res;
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
        }
        return t;
    }

    /// <summary>
    /// 数据写入
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
            if (bulkItems.Any(bulkItems => bulkItems.S7DataType == S7DataTypeEnum.Array))
            {
                await WriteToBytesAsync(bulkItems.Where(p=>p.S7DataType == S7DataTypeEnum.Array).ToArray());
            }
            else
            {
                var items = CreatWriteS7Item(bulkItems);
                try
                {
                    await _plc.WriteAsync(items);
                    Log.Logger.ForCategory(LogCategory.Net).Information($"{_plc.IP}--写入数据成功");
                }
                catch (Exception e)
                {
                    Log.Logger.ForCategory(LogCategory.Net).Information($"写入失败详细{e.Message}");
                }
            }
        }
    }
    
    public  async Task WriteToBytesAsync(WriteBufferItemInput[] bulkItems)
    {
        if (!_plc.IsConnected)
        {
            Log.Logger.ForCategory(LogCategory.Net).Information($"{_plc.IP}--PLC未连接");
        }
        else
        {
            foreach (var arrayType in bulkItems)
            {
                var dbType = EnumConvert.S7BlockTypeToDataType(arrayType.S7BlockType);
                await _plc.WriteBytesAsync(dbType,arrayType.DBAddress,arrayType.DBStart,arrayType.Buffer);
            }
        }
    }
    public static DataItem CreatReadS7Item(ReadBufferInput input)
    {
        var dbType = EnumConvert.S7BlockTypeToDataType(input.S7BlockType);
        var bulkItem = new DataItem
        {
            DB = input.DBAddress,
            StartByteAdr = input.DBStart,
            BitAdr = input.DBBit,
            Count = input.DBEnd - input.DBStart,
            DataType = dbType
        };
        return bulkItem;
    }

    public static DataItem[] CreatWriteS7Item(WriteBufferItemInput[] inputs)
    {
        List<DataItem> items = new();
        foreach (var input in inputs)
        {
            if(input.S7DataType == S7DataTypeEnum.Array)
            {
                // 处理数组类型的写入
                // 这里可以根据具体的需求进行实现
                continue;
            }
            var dbType = EnumConvert.S7BlockTypeToDataType(input.S7BlockType);
            var s7DataType =  EnumConvert.S7DataTypeToVarType(input.S7DataType);
            var bulkItem = new DataItem
            {
                DB = input.DBAddress,
                StartByteAdr = input.DBStart,
                BitAdr = input.DBBit,
                DataType = dbType,
                VarType = s7DataType,
                Value = input.Value
            };
            items.Add(bulkItem);
        }
        return items.ToArray();
    }
}