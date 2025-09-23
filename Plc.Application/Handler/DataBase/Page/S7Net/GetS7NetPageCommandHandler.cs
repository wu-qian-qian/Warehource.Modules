using AutoMapper;
using Common.Application.MediatR.Message.PageQuery;
using Common.Helper;
using Common.Shared;
using Plc.Contracts.Respon;
using Plc.Domain.S7;

namespace Plc.Application.Handler.DataBase.Page.S7Net;

internal class GetS7NetPageCommandHandler(IS7NetManager _netManager, IMapper _mapper)
    : IPageHandler<GetS7NetPageCommand, S7NetDto>
{
    public Task<PageResult<S7NetDto>> Handle(GetS7NetPageCommand request, CancellationToken cancellationToken)
    {
        var query = _netManager.GetQueryNetConfig()
            .WhereIf(request.Ip != null, p => p.Ip.Contains(request.Ip))
            .WhereIf(request.S7Type != null, p => p.S7Type == request.S7Type)
            .WhereIf(request.StartTime != null, p => p.CreationTime > request.StartTime)
            .WhereIf(request.EndTime != null, p => p.CreationTime <= request.EndTime);
        var count = query.Count();
        var data = query.ToPageBySortAsc(request.SkipCount, count, p => p.CreationTime).ToArray();
        var list = _mapper.Map<List<S7NetDto>>(data);
        return Task.FromResult(new PageResult<S7NetDto>(count, list));
    }
}