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
internal class WritelPlcEventHandle(INetService netService, ICacheService cacheService)
    : ICommandHandler<ReadPlcEventCommand>
{
    /// <summary>
    ///     进行一些操作
    ///     Todo  添加方式将读取的数据以json的方式存储
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task Handle(ReadPlcEventCommand request, CancellationToken cancellationToken)
    {
        var memory = new List<byte>();
        foreach (var input in request.readBufferInputs)
        {
            var buffer = await netService.ReadAsync(input);
            memory.AddRange(buffer);
        }

        await cacheService.SetAsync(request.DeviceName.GetHashCode().ToString(), memory.ToArray());
        Log.Logger.ForCategory(LogCategory.Net)
            .Error($"IP:{request.Ip} 设备名称{request.DeviceName}未找到对应的DB块或地址");
    }
}