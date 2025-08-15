using AutoMapper;
using Common.Application.MediatR.Behaviors;
using Common.Application.MediatR.Message;
using Plc.Contracts.Respon;
using Plc.Domain.S7;

namespace Plc.Application.Handler.DataBase.Get.Net;

internal class GetS7NetQueryHandler(IS7NetManager netManager, IMapper mapper)
    : IQueryHandler<GetS7NetQuery, Result<IEnumerable<S7NetDto>>>
{
    public async Task<Result<IEnumerable<S7NetDto>>> Handle(GetS7NetQuery request, CancellationToken cancellationToken)
    {
        Result<IEnumerable<S7NetDto>> result = new();
        var netList = await netManager.GetAllNetAsync();
        result.SetValue(mapper.Map<IEnumerable<S7NetDto>>(netList));
        return result;
    }
}