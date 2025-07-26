using Common.Application.Log;
using Common.Shared;
using Plc.Contracts.Input;
using Plc.Contracts.Respon;
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

    public override Task<bool> WriteAsync(WriteBufferInput[] bulkItems)
    {
        if (!_plc.IsConnected)
        {
            Log.Logger.ForCategory(LogCategory.Net).Information($"{_plc.IP}--PLC未连接");
            return Task.FromResult(false);
        }
        else
        {
            var arrayType = bulkItems.FirstOrDefault(bulkItems => bulkItems.S7DataType == S7DataTypeEnum.Array);
            if (arrayType != null)
            {
                var dbType = arrayType.S7BlockType switch
                {
                    S7BlockTypeEnum.DataBlock => DataType.DataBlock,
                    S7BlockTypeEnum.Memory => DataType.Memory,
                    S7BlockTypeEnum.Input => DataType.Input,
                    _ => throw new AggregateException("无法解析")
                };
              
                _plc.WriteAsync(dbType,arrayType.DBAddress,arrayType.DBStart,arrayType.Value);
            }
            else
            {
                var items = CreatWriteS7Item(bulkItems);
                try
                {
                    _plc.WriteAsync(items);
                    Log.Logger.ForCategory(LogCategory.Net).Information($"{_plc.IP}--写入数据成功");
                    return Task.FromResult(true);
                }
                catch (Exception e)
                {
                    Log.Logger.ForCategory(LogCategory.Net).Information($"写入失败详细{e.Message}");
                }
            }
            return Task.FromResult(false);
        }
    }

    public static DataItem CreatReadS7Item(ReadBufferInput input)
    {
        var dbType = input.S7BlockType switch
        {
            S7BlockTypeEnum.DataBlock => DataType.DataBlock,
            S7BlockTypeEnum.Memory => DataType.Memory,
            S7BlockTypeEnum.Input => DataType.Input,
            _ => throw new AggregateException("无法解析")
        };
        var bulkItem = new DataItem
        {
            DB = input.DBAddress,
            StartByteAdr = input.DBStart,
            Count = input.DBEnd - input.DBStart,
            DataType = dbType
        };
        return bulkItem;
    }

    public static DataItem[] CreatWriteS7Item(WriteBufferInput[] inputs)
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

            var dbType = input.S7BlockType switch
            {
                S7BlockTypeEnum.DataBlock => DataType.DataBlock,
                S7BlockTypeEnum.Memory => DataType.Memory,
                S7BlockTypeEnum.Input => DataType.Input,
                _ => throw new AggregateException("无法解析")
            };

            var s7DataType = input.S7DataType switch
            {
                S7DataTypeEnum.Bool => VarType.Bit,
                S7DataTypeEnum.Byte => VarType.Byte,
                S7DataTypeEnum.Word => VarType.Word,
                S7DataTypeEnum.DWord => VarType.DWord,
                S7DataTypeEnum.Int => VarType.Int,
                S7DataTypeEnum.DInt => VarType.DInt,
                S7DataTypeEnum.Real => VarType.Real,
                S7DataTypeEnum.LReal => VarType.LReal,
                S7DataTypeEnum.String => VarType.String,
                S7DataTypeEnum.S7String => VarType.S7String,
                _ => throw new AggregateException("无法解析")
            };
            var bulkItem = new DataItem
            {
                DB = input.DBAddress,
                StartByteAdr = input.DBStart,
                BitAdr = input.DBBit,
                DataType = dbType,
                VarType = s7DataType,
            };
            items.Add(bulkItem);
        }
        return items.ToArray();
    }
}