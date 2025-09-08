using MediatR;
using Wcs.Application.Handler.Business.ReadPlcBlock;
using Wcs.Application.Handler.DeviceExecute;

namespace Wcs.Application.Handler.Business.Behaviors;

/// <summary>
///     是否可以执行业务
///     如果此次获取到db数据业务即可执行反正不执行
///     有优化项
/// </summary>
/// <param name="sender"></param>
/// <typeparam name="TRequest"></typeparam>
/// <typeparam name="TResponse"></typeparam>
internal class CanBusinessPipelinBehavior<TRequest, TResponse>(ISender sender) : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IExecuteDeviceCommand
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        if (request.ReadPlc != null)
        {
            //TODO 添加
            var dbEntity = await sender.Send(new GetPlcDBQuery
                {
                    DeviceName = request.ReadPlc.DeviceName,
                    Key = request.ReadPlc.Key,
                    DeviceType = request.ReadPlc.DeviceType,
                    DBEntity = request.ReadPlc.DBEntity,
                    UseMemory = request.ReadPlc.UseMemory
                },
                cancellationToken);
            if (dbEntity != null) return await next();
        }

        return await next();
    }
}