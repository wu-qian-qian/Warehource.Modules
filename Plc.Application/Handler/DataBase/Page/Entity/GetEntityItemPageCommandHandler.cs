using AutoMapper;
using Common.Application.MediatR.Message.PageQuery;
using Common.Helper;
using Common.Shared;
using Plc.Contracts.Respon;
using Plc.Domain.S7;

namespace Plc.Application.Handler.DataBase.Page.Entity;

internal class GetEntityItemPageCommandHandler(IS7NetManager _netManager, IMapper _mapper)
    : IPageHandler<GetEntityItemPageCommand, S7EntityItemDto>
{
    public Task<PageResult<S7EntityItemDto>> Handle(GetEntityItemPageCommand request,
        CancellationToken cancellationToken)
    {
        var query = _netManager.GetQueryS7EntityItem()
            .WhereIf(request.DeviceName != null, p => p.DeviceName == request.DeviceName)
            .WhereIf(request.Name != null, p => p.Name == request.Name)
            .WhereIf(request.Ip != null, p => p.Ip.Contains(request.Ip))
            .WhereIf(request.StartTime != null, p => p.CreationTime > request.StartTime)
            .WhereIf(request.EndTime != null, p => p.CreationTime <= request.EndTime);
        var count = query.Count();
        var data = query.ToPageBySortAsc(request.SkipCount, count, p => p.CreationTime).ToArray();
        var list = _mapper.Map<List<S7EntityItemDto>>(data);
        return Task.FromResult(new PageResult<S7EntityItemDto>(count, list));
    }
}