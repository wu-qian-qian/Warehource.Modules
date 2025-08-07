using Common.Application.Log;
using Common.Application.MediatR.Message;
using Common.Shared;
using Plc.Application.Abstract;
using Plc.Contracts.Respon;
using Serilog;
using System;

namespace Plc.Application.PlcHandler.Read;

/// <summary>
///     最终处理者
/// </summary>
/// <param name="service"></param>
internal class ReadPlcEventHandle(INetService netService)
    : ICommandHandler<ReadPlcEventCommand, IEnumerable<ReadBuffer>>
{
    /// <summary>
    ///     进行一些操作
    ///     Todo  添加方式将读取的数据以json的方式存储
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<IEnumerable<ReadBuffer>> Handle(ReadPlcEventCommand request, CancellationToken cancellationToken)
    {
        var buffers = new ReadBuffer[request.readBufferInputs.Count()];
        for (int i = 0; i < request.readBufferInputs.Count(); i++)
        {
            var input = request.readBufferInputs[i];
            var buffer = await netService.ReadAsync(input);
            if (buffer != null)
                buffers[i] = new ReadBuffer(input.DBAddress,buffer);
            else
                Log.Logger.ForCategory(LogCategory.Net)
                    .Error($"IP:{request.Ip} 设备名称{request.DeviceName}读取失败");
        }
        return buffers;
    }
}