using Common.Application.Log;
using Common.Application.Net;
using Common.Shared;
using MediatR;
using Plc.Application.Abstract;
using Plc.Application.DBHandler.Get.Net;
using Plc.Contracts.Input;
using Plc.Contracts.Respon;
using Plc.Infrastructure.Token;
using Serilog;

namespace Plc.Infrastructure.Service;

/// <summary>
///     S7的服务类型
///     注册类型为单例
/// </summary>
public class S7NetService : INetService
{
    public readonly Dictionary<string, S7Net> NetMap = new();

    public void Initialization(ISender sender)
    {
        var netList = sender.Send(new GetS7NetQuery()).GetAwaiter().GetResult();
        if (netList.IsSuccess)
        {
            foreach (var netConfig in netList.Value)
            {
                var token = new S7NetToken(netConfig);
                AddConnect(token);
            }
        }
    }

    public void AddConnect(INet connect)
    {
        if (connect is S7Net s7Net)
        {
            if (!NetMap.ContainsKey(s7Net._plc.IP)) NetMap[s7Net._plc.IP] = s7Net;
        }
        else
        {
            Log.Logger.ForCategory(LogCategory.Net).Error($"{connect.GetType()}-服务不符合添加项");
        }
    }

    public Task<byte[]> ReadAsync(ReadBufferInput input)
    {
        return NetMap[input.Ip].ReadAsync(input);
    }

    public async Task ReConnect(S7NetDto netDto)
    {
        if (NetMap.ContainsKey(netDto.Ip) == false)
        {
            var token = new S7NetToken(netDto);
            AddConnect(token);
        }

        foreach (var net in NetMap.Values)
            if (netDto.IsUse)
            {
                if (net._plc.IsConnected)
                {
                    //写入心跳
                }
                else
                {
                    net.UsePlc(netDto.IsUse);
                }
            }
    }

    public async Task WriteAsync(WriteBufferInput input)
    {
        await NetMap[input.Ip].WriteAsync(input.WriteBufferItemArray);
    }

    public async Task<bool> CheckWriteByteAsync(WriteBufferInput input)
    {
        return await NetMap[input.Ip].CheckWriteToBytesAsync(input.WriteBufferItemArray);
    }
}