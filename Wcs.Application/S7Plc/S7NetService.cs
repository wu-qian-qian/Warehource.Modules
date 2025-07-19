using Common.Application.Log;
using Common.Application.Net;
using Common.Application.Net.S7;
using Common.Shared;
using Serilog;

namespace Wcs.Application.S7Plc;

/// <summary>
///     S7的服务类型
///     注册类型为单例
/// </summary>
public class S7NetService : INetService
{
    public readonly Dictionary<string, INet> NetMap = new();

    public void AddConnect(INet connect)
    {
        if (connect is S7Net s7Net)
        {
            if (!NetMap.ContainsKey(s7Net._plc.IP))
            {
                s7Net.Connect();
                NetMap.Add(s7Net._plc.IP, s7Net);
            }
        }
        else
        {
            Log.Logger.ForCategory(LogCategory.Net).Information($"{connect.GetType()}-服务不符合添加项");
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