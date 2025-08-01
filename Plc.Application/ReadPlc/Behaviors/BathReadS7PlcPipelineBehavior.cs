using MediatR;
using Plc.Application.ReadPlc;
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
                    var key = request.DeviceName+"Bath";
                    if (PlcReadDtoHelper._readBufferInputs.ContainsKey(key)==false)
                    {
                        var s7EntityItems = await netManager.GetNetWiteDeviceNameAsync(request.DeviceName);
                        PlcReadDtoHelper.UseMemoryInitBufferInput(key,s7EntityItems.ToArray());
                    }
                    request.readBufferInputs = PlcReadDtoHelper._readBufferInputs[key];
                }
                else
                {
                    //将该ip的数据 加载到缓存
                    if (!PlcReadDtoHelper._readBufferInputs.ContainsKey(request.Ip))
                    {
                        var netConfig = await netManager.GetNetWiteIpAsync(request.Ip);
                        PlcReadDtoHelper.UseMemoryInitBufferInput(netConfig, request.Ip);
                    }
                    request.readBufferInputs = PlcReadDtoHelper._readBufferInputs[request.Ip];
                }
            }
            else
            {
                if (request.DeviceName != null)
                {
                    var s7EntityItems = await netManager.GetNetWiteDeviceNameAsync(request.DeviceName);
                    request.readBufferInputs = PlcReadDtoHelper.CreatBufferInput(s7EntityItems.ToArray());
                }
                else
                {
                   var netConfig = await netManager.GetNetWiteIpAsync(request.Ip);
                   request.readBufferInputs = PlcReadDtoHelper.InitBufferInput(netConfig);
                }
            }
        }

        return await next();
    }
}