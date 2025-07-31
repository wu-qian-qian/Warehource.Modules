using MediatR;
using Plc.Application.ReadPlc;
using Plc.Domain.S7;

namespace Plc.Application.Behaviors;

/// <summary>
///     单变量读取配置
/// </summary>
internal class SingleReadS7PlcPipelineBehavior<TRequest, TResponse>(IS7NetManager netManager)
    : IPipelineBehavior<TRequest, TResponse> where TRequest : PlcEventCommand
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        if (request.IsBath == false)
        {
            if (request.UseMemory)
            {
                string key = request.Ip + request.DeviceName;
                if (!PlcReadDtoHelper._readBufferInputs.ContainsKey(key))
                {
                    var netConfig = await netManager.GetNetWiteDeviceNameAsync(request.Ip,request.DeviceName);
                    PlcReadDtoHelper.UseMemoryInitBufferInput(netConfig,key);
                }
                request.readBufferInputs = PlcReadDtoHelper._readBufferInputs[request.Ip];
            }
            else
            {
                var  netConfig = await netManager.GetNetWiteDeviceNameAsync(request.Ip,request.DeviceName);
                netConfig.S7EntityItems = netConfig.S7EntityItems
                    .Where(p=>request.DBNames.Contains(p.Name)).ToArray();
                request.readBufferInputs = PlcReadDtoHelper.InitBufferInput(netConfig);
            }
        }
        return await next();
    }
}