using Common.Application.Caching;
using Common.Application.Log;
using Common.Application.MediatR.Message;
using Common.Shared;
using Plc.Application.Abstract;
using Plc.Contracts.Input;
using Plc.Domain.S7;
using Plc.Shared;

namespace Plc.Application.S7Plc.ReadPlc;

/// <summary>
///     最终处理者
/// </summary>
/// <param name="service"></param>
internal class ReadlPlcEventHandle(INetService netService
    ,ICacheService cacheService) : ICommandHandler<PlcEventCommand>
{
    /// <summary>
    ///     进行一些操作
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task Handle(PlcEventCommand request, CancellationToken cancellationToken)
    {
        var inputs = request.readBufferInputs;
        if (request.DeviceName == null)
        {
            List<byte> memory = new List<byte>();
            foreach (var input in inputs)
            {
                var buffer = await netService.ReadAsync(input);
                memory.AddRange(buffer);
            }
            await cacheService.SetAsync(request.Ip.GetHashCode().ToString(), memory.ToArray());
        }
        else
        {
            var input = inputs.FirstOrDefault(p => p.HashId == request.DeviceName.GetHashCode());
            if (input != null)
            {
                var buffer = await netService.ReadAsync(input);
                await cacheService.SetAsync(request.DeviceName.GetHashCode().ToString(), buffer);
            }
            else
            {
                Serilog.Log.Logger.ForCategory(LogCategory.Net)
                    .Error($"IP:{request.Ip} 设备名称{request.DeviceName}未找到对应的DB块或地址");
            }
        }
    }

  
}