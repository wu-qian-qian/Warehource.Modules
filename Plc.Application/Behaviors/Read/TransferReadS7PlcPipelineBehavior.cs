using Common.Application.Caching;
using MediatR;
using Plc.Application.Handler.ReadWrite;
using Plc.Application.Handler.ReadWrite.Read;
using Plc.Contracts.DataModel;
using Plc.Contracts.Respon;
using Plc.Domain.S7;

namespace Plc.Application.Behaviors.Read;

/// <summary>
///     自定义存储方式
///     个人通过自己程序进行配置
/// </summary>
internal class TransferReadS7PlcPipelineBehavior(
    ICacheService cacheService,
    IS7NetManager netManager)
    : IPipelineBehavior<ReadPlcEventCommand, IEnumerable<ReadBuffer>>
{
    public async Task<IEnumerable<ReadBuffer>> Handle(ReadPlcEventCommand request,
        RequestHandlerDelegate<IEnumerable<ReadBuffer>> next,
        CancellationToken cancellationToken)
    {
        var key = request.Key;
        //映射数据存入缓存
        var mapkey = $"{request.Key}#map";
        //获取到映射转换器 
        var memoryEntity = await cacheService.GetAsync<IEnumerable<EntityItemModel>>(mapkey);
        if (memoryEntity == null)
        {
            var entityItems = (await netManager.GetNetWiteDeviceNameAsync(request.DeviceName))
                .OrderBy(p => p.Index);
            memoryEntity = entityItems.Select(p =>
            {
                return new EntityItemModel(p.Name, p.S7DataType, p.DBAddress, p.DataOffset, p.BitOffset,
                    p.ArrayLength);
            });
            await cacheService.SetAsync(mapkey, memoryEntity);
        }

        //公共模块触发，将数据放入缓存
        if (request.IsApi == false)
        {
            var response = await next();
            if (response != null)
            {
                var readModels = new List<ReadModel>();
                foreach (var item in response)
                {
                    var filterEntity = memoryEntity.Where(p => p.DBAddress == item.DB);
                    var readModelArray = filterEntity.Select(p =>
                    {
                        var data = TransferDataHelper.TransferBufferToData(item.Data, p.OffSet, p.BitOffSet,
                            p.S7DataType, p.ArrayLength);
                        return new ReadModel(p.DBName, data);
                    });
                    readModels.AddRange(readModelArray);
                }

                await cacheService.SetAsync<IEnumerable<ReadModel>>(key, readModels);
            }
        }

        return await next();
    }
}