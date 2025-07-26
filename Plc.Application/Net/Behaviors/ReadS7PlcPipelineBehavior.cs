using MediatR;
using Plc.Application.Net.ReadPlc;
using Plc.Domain.S7;

namespace Plc.Application.Net.Behaviors;

/// <summary>
///     ReadlPlcEventHandle 的中间管道，用来做一些解析
///     如一些特殊的配置
/// </summary>
internal class ReadS7PlcPipelineBehavior<TRequest, TResponse>(IS7NetManager netManager)
    : IPipelineBehavior<TRequest, TResponse> where TRequest : PlcEventCommand
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        if (request.UseMemory)
        {
            if (!MemoryReadPlc._readBufferInputs.ContainsKey(request.Ip))
            {
                var netConfig = await netManager.GetNetWiteIpAsync(request.Ip);
                MemoryReadPlc.InitBufferInput(netConfig);
            }

            request.readBufferInputs = MemoryReadPlc._readBufferInputs[request.Ip];
        }
        else
        {
            var netConfig=default(S7NetConfig);
            if (request.DeviceName != null)
            {
                netConfig = await netManager.GetNetWiteIpAsync(request.Ip,request.DeviceName);
            }
            else
            {
                netConfig = await netManager.GetNetWiteIpAsync(request.Ip);
            }
            request.readBufferInputs = MemoryReadPlc.GetBufferInput(netConfig);
        }
        return await next();
    }
}