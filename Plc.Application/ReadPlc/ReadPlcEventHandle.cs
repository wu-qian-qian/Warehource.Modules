using System.Buffers;
using Common.Application.Caching;
using Common.Application.Log;
using Common.Application.MediatR.Message;
using Common.Shared;
using Plc.Application.Abstract;
using Serilog;

namespace Plc.Application.ReadPlc;

/// <summary>
///     最终处理者
/// </summary>
/// <param name="service"></param>
internal class ReadPlcEventHandle(INetService netService)
    : ICommandHandler<ReadPlcEventCommand,byte[]>
{
    /// <summary>
    ///     进行一些操作
    ///     Todo  添加方式将读取的数据以json的方式存储
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<byte[]> Handle(ReadPlcEventCommand request, CancellationToken cancellationToken)
    {
        List<byte> memory = new();
        foreach (var input in request.readBufferInputs)
        {
            var buffer = await netService.ReadAsync(input);
            memory.AddRange(buffer);
        }
        Log.Logger.ForCategory(LogCategory.Net)
            .Error($"IP:{request.Ip} 设备名称{request.DeviceName}读取成功");
        return memory.ToArray();
    }
}