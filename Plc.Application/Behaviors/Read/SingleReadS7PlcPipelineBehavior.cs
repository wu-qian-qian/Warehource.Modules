using MediatR;
using Plc.Application.Handler.ReadWrite.Read;
using Plc.Contracts.Respon;
using Plc.Domain.S7;

namespace Plc.Application.Behaviors.Read;

/// <summary>
///     单变量读取配置
/// </summary>
internal class SingleReadS7PlcPipelineBehavior(IS7NetManager netManager)
    : IPipelineBehavior<ReadPlcEventCommand, IEnumerable<ReadBuffer>>
{
    public async Task<IEnumerable<ReadBuffer>> Handle(ReadPlcEventCommand request,
        RequestHandlerDelegate<IEnumerable<ReadBuffer>> next,
        CancellationToken cancellationToken)
    {
        if (request.IsBath == false)
        {
            //因为是缓存所以会预加载所有
            if (request.UseMemory)
            {
                //加载所有的变量模型
                var key = request.DeviceName + "Sigle";
                if (PlcReadWriteDtoHelper._readBufferInputs.ContainsKey(key) == false)
                {
                    var s7EntityItems =
                        (await netManager.GetNetWiteDeviceNameAsync(request.DeviceName)).OrderBy(p => p.Index);
                    PlcReadWriteDtoHelper.UseMemoryInitReadBufferInput(key, s7EntityItems.ToArray());
                }

                //筛选出指定的模型
                request.readBufferInputs = PlcReadWriteDtoHelper._readBufferInputs[key]
                    .Where(p => request.DBNames.Contains(p.HashId)).ToArray();
            }
            else
            {
                //直接获取到指定的变量模型
                var s7EntityItems =
                    (await netManager.GetDeviceNameWithDBNameAsync(request.DeviceName, request.DBNames.ToList()))
                    .OrderBy(p => p.Index);
                request.readBufferInputs =
                    PlcReadWriteDtoHelper.CreatReadBufferInput(s7EntityItems.ToArray()).ToArray();
            }
        }

        return await next();
    }
}