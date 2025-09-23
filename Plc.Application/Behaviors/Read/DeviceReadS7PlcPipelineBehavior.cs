using MediatR;
using Plc.Application.Handler.ReadWrite.Read;
using Plc.Contracts.Respon;
using Plc.Domain.S7;

namespace Plc.Application.Behaviors.Read;

/// <summary>
///     批量读取的配置
/// </summary>
internal class DeviceReadS7PlcPipelineBehavior(IS7NetManager netManager)
    : IPipelineBehavior<ReadPlcEventCommand, IEnumerable<ReadBuffer>>
{
    public async Task<IEnumerable<ReadBuffer>> Handle(ReadPlcEventCommand request,
        RequestHandlerDelegate<IEnumerable<ReadBuffer>> next,
        CancellationToken cancellationToken)
    {
        if (request.IsBath)
        {
            if (request.UseMemory)
            {
                if (request.DeviceName != null)
                {
                    //将该设备模型加载到内存
                    var key = request.DeviceName + "Bath";
                    if (PlcReadWriteDtoHelper._readBufferInputs.ContainsKey(key) == false)
                    {
                        var s7EntityItems = await netManager.GetNetWiteDeviceNameAsync(request.DeviceName);
                        PlcReadWriteDtoHelper.UseMemoryInitReadBufferInput(key, s7EntityItems.ToArray());
                    }

                    request.readBufferInputs = PlcReadWriteDtoHelper._readBufferInputs[key].ToArray();
                }
            }
            else
            {
                if (request.DeviceName != null)
                {
                    var s7EntityItems = await netManager.GetNetWiteDeviceNameAsync(request.DeviceName);
                    request.readBufferInputs =
                        PlcReadWriteDtoHelper.CreatReadBufferInput(s7EntityItems.ToArray()).ToArray();
                }
            }
        }

        return await next();
    }
}