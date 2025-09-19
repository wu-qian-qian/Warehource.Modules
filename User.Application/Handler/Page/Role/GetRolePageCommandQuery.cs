using AutoMapper;
using Common.Application.MediatR.Message.PageQuery;
using Common.Helper;
using Common.Shared;
using Identity.Contrancts.Respon;
using Identity.Domain;

namespace Identity.Application.Handler.Page.Role;

internal class GetRolePageCommandQuery(UserManager _userManager, IMapper _mapper)
    : IPageHandler<GetRolePageCommand, RoleDto>
{
    public async Task<PageResult<RoleDto>> Handle(GetRolePageCommand request, CancellationToken cancellationToken)
    {
        var query = await _userManager.GetRoleQueryAsync();
        query = query
            .WhereIf(request.RoleName != null, x => x.RoleName.Contains(request.RoleName))
            .WhereIf(request.StartTime != null, x => x.CreationTime > request.StartTime)
            .WhereIf(request.EndTime != null, x => x.CreationTime < request.EndTime);
        var count = query.Count();
        var data = query.ToPageBySortAsc(request.SkipCount, request.Total, p => p.CreationTime).ToList();
        var list = _mapper.Map<List<RoleDto>>(data);
        return new PageResult<RoleDto>(count, list);
    }
}