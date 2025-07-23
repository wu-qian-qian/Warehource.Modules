using AutoMapper;
using Common.Application.MediatR.Message;
using Wcs.Contracts.S7Plc;
using Wcs.Domain.S7;

namespace Wcs.Application.S7Plc.Get;

internal class GetS7NetQueryHandler(IS7NetManager netManager, IMapper mapper)
    : IQueryHandler<GetS7NetQuery, IEnumerable<S7NetDto>>
{
    public async Task<IEnumerable<S7NetDto>> Handle(GetS7NetQuery request, CancellationToken cancellationToken)
    {
        var netList = await netManager.GetAllNet();
        return mapper.Map<IEnumerable<S7NetDto>>(netList);
    }
}