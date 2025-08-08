using MediatR;
using Plc.Application.PlcHandler.Read;
using Plc.Contracts.Input;
using Plc.Domain.S7;

namespace Plc.Application.Behaviors.Read;

/// <summary>
///     批量读取的配置
/// </summary>
internal class BathReadS7PlcPipelineBehavior<TRequest, TResponse>(IS7NetManager netManager)
    : IPipelineBehavior<TRequest, TResponse> where TRequest : ReadPlcEventCommand
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next,
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
                else
                {
                    //将该ip的数据 加载到缓存
                    if (!PlcReadWriteDtoHelper._readBufferInputs.ContainsKey(request.Ip))
                    {
                        var netConfig = await netManager.GetNetWiteIpAsync(request.Ip);
                        PlcReadWriteDtoHelper.UseMemoryInitReadBufferInput(netConfig, request.Ip);
                    }

                    request.readBufferInputs = PlcReadWriteDtoHelper._readBufferInputs[request.Ip].ToArray();
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
                else
                {
                    var netConfig = await netManager.GetNetWiteIpAsync(request.Ip);
                    var readBufferInputs = new List<ReadBufferInput>();
                    PlcReadWriteDtoHelper.CreatReadBufferInput(netConfig, readBufferInputs);
                    request.readBufferInputs = readBufferInputs.ToArray();
                }
            }
        }

        return await next();
    }
}