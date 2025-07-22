using AutoMapper;
using Common.Application.MediatR.Message;
using Wcs.Domain.S7;

namespace Wcs.Application.S7Plc.Get;

internal class GetS7NetQueryHandler(IS7NetManager netManager, IMapper mapper)
    : IQueryHandler<GetS7NetQuery, IEnumerable<S7NetConfig>>
{
    public async Task<IEnumerable<S7NetConfig>> Handle(GetS7NetQuery request, CancellationToken cancellationToken)
    {
        var netList = await netManager.GetAllNet();
        return netList;
    }
}