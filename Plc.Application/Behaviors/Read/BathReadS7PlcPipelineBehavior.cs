using MediatR;
using Plc.Application.ReadPlc;
using Plc.Domain.S7;

namespace Plc.Application.Behaviors;

/// <summary>
///     批量读取的配置
/// </summary>
internal class BathReadS7PlcPipelineBehavior<TRequest, TResponse>(IS7NetManager netManager)
    : IPipelineBehavior<TRequest, TResponse> where TRequest : PlcEventCommand
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        if (request.IsBath == true)
        {
            //第一次会把该地址下所有的变量加载
            if (request.UseMemory)
            {
                if (!PlcReadDtoHelper._readBufferInputs.ContainsKey(request.Ip))
                {
                    var netConfig = await netManager.GetNetWiteIpAsync(request.Ip);
                    PlcReadDtoHelper.UseMemoryInitBufferInput(netConfig,request.Ip);
                }
                request.readBufferInputs = PlcReadDtoHelper._readBufferInputs[request.Ip];
            }
            else
            {
                var netConfig=default(S7NetConfig);
                if (request.DeviceName != null)
                {
                    netConfig = await netManager.GetNetWiteDeviceNameAsync(request.Ip,request.DeviceName);
                }
                else
                {
                    netConfig = await netManager.GetNetWiteIpAsync(request.Ip);
                }
                request.readBufferInputs = PlcReadDtoHelper.InitBufferInput(netConfig);
            }
        }
        return await next();
    }
}