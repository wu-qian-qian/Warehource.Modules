using Common.Application.Caching;
using MediatR;
using Plc.Application.S7ReadWriteHandler;
using Plc.Application.S7ReadWriteHandler.Read;
using Plc.Contracts.DataModel;
using Plc.Contracts.Respon;
using Plc.Domain.S7;

namespace Plc.Application.Behaviors.Read;

/// <summary>
///     自定义存储方式
///     个人通过自己程序进行配置
/// </summary>
internal class TransferReadS7PlcPipelineBehavior<TRequest, TResponse>(
    ICacheService cacheService,
    IS7NetManager netManager)
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : ReadPlcEventCommand
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        var key = string.Empty;
        if (request.IsBath)
        {
            if (request.DeviceName != null)
                key = request.Id != null ? request.Id.ToString() : request.DeviceName;
            else
                key = request.Id != null ? request.Id.ToString() : request.Ip;
        }
        else
        {
            key = request.Id.ToString();
        }

        //映射数据存入缓存
        var memoryEntity = await cacheService.GetAsync<IEnumerable<EntityItemModel>>(key + "Transfer");
        if (memoryEntity == null)
        {
            var entityItems = (await netManager.GetNetWiteDeviceNameAsync(request.DeviceName)).OrderBy(p => p.Index);
            var entityItemModels = entityItems.Select(p =>
            {
                return new EntityItemModel(p.Name, p.S7DataType, p.DBAddress, p.DataOffset, p.BitOffset,
                    p.ArrayLength);
            });
            await cacheService.SetAsync(key + "Transfer", entityItemModels);
        }

        var response = await next();
        //公共模块触发，将数据放入缓存
        if (request.IsApi == false)
            if (response is IEnumerable<ReadBuffer> tempbuffer)
            {
                var readModels = new List<ReadModel>();
                foreach (var item in tempbuffer)
                {
                    var filterEntity = memoryEntity.Where(p => p.DBAddress == item.DB);
                    var readModelArray = filterEntity.Select(p =>
                    {
                        var data = TransferDataHelper.TransferBufferToData(item.Data, p.OffSet, p.BitOffSet.Value,
                            p.S7DataType, p.ArrayLength);
                        return new ReadModel(p.DBName, data);
                    });
                    readModels.AddRange(readModels);
                }

                await cacheService.SetAsync<IEnumerable<ReadModel>>(key, readModels);
            }

        return response;
    }
}