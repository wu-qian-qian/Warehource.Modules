using AutoMapper;
using Common.Application.MediatR.Behaviors;
using Common.Application.MediatR.Message;
using Plc.Contracts.Respon;
using Plc.Domain.S7;

namespace Plc.Application.DBHandler.Get.Entity;

public class GetS7EntityItemQueryHandler(IS7NetManager netManager, IMapper mapper)
    : IQueryHandler<GetS7EntityItemQuery,Result<IEnumerable<S7EntityItemDto>>>
{
    public async Task<Result<IEnumerable<S7EntityItemDto>>> Handle(GetS7EntityItemQuery request,
        CancellationToken cancellationToken)
    {
        Result<IEnumerable<S7EntityItemDto>> result = new();
        var entityItemList = await netManager.GetAllNetEntityItemAsync();
        result.Value= mapper.Map<IEnumerable<S7EntityItemDto>>(entityItemList);
        result.IsSuccess = true;
        return result;
    }
}