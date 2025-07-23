using Common.Application.Log;
using Common.Application.Net;
using Common.Shared;
using MediatR;
using Plc.Application.Abstract;
using Plc.Application.S7Plc.Get;
using Serilog;

namespace Plc.Infrastructure.S7Net;

/// <summary>
///     S7的服务类型
///     注册类型为单例
/// </summary>
public class S7NetService : INetService
{
    public readonly Dictionary<string, INet> NetMap = new();

    public void Initialization(ISender sender)
    {
        try
        {
            var netList = sender.Send(new GetS7NetQuery()).GetAwaiter().GetResult();
            foreach (var netConfig in netList)
            {
                var token = new S7NetToken(netConfig);
                AddConnect(token);
            }
        }
        catch (Exception e)
        {
            Log.Logger.ForCategory(LogCategory.Net).Error($"{e.Message}-无法初始化服务");
        }
    }

    public void AddConnect(INet connect)
    {
        if (connect is Common.Application.Net.S7.S7Net s7Net)
        {
            if (!NetMap.ContainsKey(s7Net._plc.IP))
            {
                s7Net.Connect();
                NetMap.Add(s7Net._plc.IP, s7Net);
            }
        }
        else
        {
            Log.Logger.ForCategory(LogCategory.Net).Error($"{connect.GetType()}-服务不符合添加项");
        }
    }

    public Task<byte[]> ReadAsync()
    {
        throw new NotImplementedException();
    }

    public async Task ReConnect()
    {
        //通过后台job执行，并进行心跳处理
    }

    public Task<bool> WriteAsync()
    {
        throw new NotImplementedException();
    }
}