using AutoMapper;
using Common.Application.MediatR.Message;
using Wcs.Contracts.S7Plc;
using Wcs.Domain.S7;

namespace Wcs.Application.S7Plc.Get;

public class GetS7EntityItemQueryHandler(IS7NetManager netManager, IMapper mapper)
    :IQueryHandler<GetS7EntityItemQuery,IEnumerable<S7EntityItemDto>>
{
    public async Task<IEnumerable<S7EntityItemDto>> Handle(GetS7EntityItemQuery request, CancellationToken cancellationToken)
    {
       var entityItemList=await netManager.GetAllNetEntityItem();
       return mapper.Map<IEnumerable<S7EntityItemDto>>(entityItemList);
    }
}