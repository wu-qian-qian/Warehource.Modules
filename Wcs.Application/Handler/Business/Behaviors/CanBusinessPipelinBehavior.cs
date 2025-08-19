using MediatR;
using Wcs.Application.Handler.Business.DeviceExecute.Stacker;
using Wcs.Application.Handler.Business.DeviceExecute.StackerTranshipIn;
using Wcs.Application.Handler.Business.DeviceExecute.StackerTranshipOut;
using Wcs.Application.Handler.Business.DeviceExecute.StockIn;
using Wcs.Application.Handler.Business.DeviceExecute.StockOut;
using Wcs.Application.Handler.Business.ReadPlcBlock;
using Wcs.Device.Abstract;
using Wcs.Device.DataBlock;
using Wcs.Shared;

namespace Wcs.Application.Handler.Business.Behaviors;

/// <summary>
///     是否可以执行业务
///     如果此次获取到db数据业务即可执行反正不执行
/// </summary>
/// <param name="sender"></param>
/// <typeparam name="TRequest"></typeparam>
/// <typeparam name="TResponse"></typeparam>
internal class CanBusinessPipelinBehavior<TRequest, TResponse>(ISender sender) : IPipelineBehavior<TRequest, TResponse>
    where TResponse : BaseDBEntity
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        var dbEntity = default(BaseDBEntity);
        if (request is StackerCommand stacker)
        {
            dbEntity = await sender.Send(new GetPlcDBQuery
                {
                    DeviceName = stacker.Stacker.Name,
                    Key = stacker.Stacker.Config.DBKey,
                    DeviceType = DeviceTypeEnum.Stacker,
                    DBEntity = stacker.Stacker.DBEntity
                },
                cancellationToken);
            stacker.Stacker.SetDBEntity((StackerDBEntity)dbEntity);
        }
        else if (request is StockInCommand stockIn)
        {
            dbEntity = await sender.Send(new GetPlcDBQuery
                {
                    DeviceName = stockIn.StockIn.Name,
                    Key = stockIn.StockIn.Config.DBKey,
                    DeviceType = DeviceTypeEnum.Stacker,
                    DBEntity = stockIn.StockIn.DBEntity
                },
                cancellationToken);
            stockIn.StockIn.SetDBEntity((PipeLineDBEntity)dbEntity);
        }
        else if (request is StockOutCommand stockOut)
        {
            dbEntity = await sender.Send(new GetPlcDBQuery
                {
                    DeviceName = stockOut.StockOut.Name,
                    Key = stockOut.StockOut.Config.DBKey,
                    DeviceType = DeviceTypeEnum.Stacker,
                    DBEntity = stockOut.StockOut.DBEntity
                },
                cancellationToken);
            stockOut.StockOut.SetDBEntity((PipeLineDBEntity)dbEntity);
        }
        else if (request is StackerTranshipInCommand transhipIn)
        {
            dbEntity = await sender.Send(new GetPlcDBQuery
                {
                    DeviceName = transhipIn.InTranShip.Name,
                    Key = transhipIn.InTranShip.Config.DBKey,
                    DeviceType = DeviceTypeEnum.Stacker,
                    DBEntity = transhipIn.InTranShip.DBEntity
                },
                cancellationToken);
            transhipIn.InTranShip.SetDBEntity((PipeLineDBEntity)dbEntity);
        }
        else if (request is StackerTranshipOutCommand transhipOut)
        {
            dbEntity = await sender.Send(new GetPlcDBQuery
            {
                DeviceName = transhipOut.OutTranShip.Name,
                Key = transhipOut.OutTranShip.Config.DBKey,
                DeviceType = DeviceTypeEnum.Stacker,
                DBEntity = transhipOut.OutTranShip.DBEntity
            }, cancellationToken);
            transhipOut.OutTranShip.SetDBEntity((PipeLineDBEntity)dbEntity);
        }

        if (dbEntity != null)
            return await next();
        return default;
    }
}