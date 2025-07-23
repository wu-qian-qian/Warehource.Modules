using AutoMapper;
using Common.Application.MediatR.Message;
using Plc.Contracts.S7Plc;
using Plc.Domain.S7;

namespace Plc.Application.S7Plc.Get;

public class GetS7EntityItemQueryHandler(IS7NetManager netManager, IMapper mapper)
    : IQueryHandler<GetS7EntityItemQuery, IEnumerable<S7EntityItemDto>>
{
    public async Task<IEnumerable<S7EntityItemDto>> Handle(GetS7EntityItemQuery request,
        CancellationToken cancellationToken)
    {
        var entityItemList = await netManager.GetAllNetEntityItem();
        return mapper.Map<IEnumerable<S7EntityItemDto>>(entityItemList);
    }
}