using AutoMapper;
using Common.Application.MediatR.Message;
using Plc.Contracts.Respon;
using Plc.Domain.S7;

namespace Plc.Application.PlcEvent.Get.Net;

internal class GetS7NetQueryHandler(IS7NetManager netManager, IMapper mapper)
    : IQueryHandler<GetS7NetQuery, IEnumerable<S7NetDto>>
{
    public async Task<IEnumerable<S7NetDto>> Handle(GetS7NetQuery request, CancellationToken cancellationToken)
    {
        var netList = await netManager.GetAllNetAsync();
        return mapper.Map<IEnumerable<S7NetDto>>(netList);
    }
}