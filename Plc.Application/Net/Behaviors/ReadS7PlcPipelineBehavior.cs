using MediatR;
using Plc.Application.Net.Behaviors;
using Plc.Application.S7Plc.ReadPlc;
using Plc.Contracts.Input;
using Plc.Domain.S7;

namespace Plc.Application.S7Plc.Behaviors;

/// <summary>
///     ReadlPlcEventHandle 的中间管道，用来做一些解析
///     如一些特殊的配置
/// </summary>
internal class ReadS7PlcPipelineBehavior<TRequest, TResponse>(IS7NetManager netManager) : IPipelineBehavior<TRequest, TResponse> where  TRequest: PlcEventCommand
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        if (request.UseMemory == true)
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
            var netConfig = await netManager.GetNetWiteIpAsync(request.Ip);
            request.readBufferInputs = MemoryReadPlc.GetBufferInput(netConfig);
        }
        return await next();
    }
}