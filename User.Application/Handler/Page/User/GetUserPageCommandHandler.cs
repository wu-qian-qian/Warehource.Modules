using AutoMapper;
using Common.Application.MediatR.Message.PageQuery;
using Common.Helper;
using Common.Shared;
using Identity.Application.Handler.Page.Role;
using Identity.Contrancts;
using Identity.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Identity.Contrancts.Respon;

namespace Identity.Application.Handler.Page.User
{
    internal class GetUserPageCommandHandler(UserManager _userManager, IMapper _mapper) : IPageHandler<GetUserPageCommand, UserDto>
    {
        public async Task<PageResult<UserDto>> Handle(GetUserPageCommand request, CancellationToken cancellationToken)
        {
            var query = await _userManager.GetUserQueryAsync();
            query = query.WhereIf(request.UserName != null, x => x.Name.Contains(request.UserName))
                 .WhereIf(request.StartTime != null, x => x.CreationTime > request.StartTime)
                .WhereIf(request.EndTime != null, x => x.CreationTime < request.EndTime);
            var count = query.Count();
            var data = query.ToPageBySortAsc(request.SkipCount, request.Total, p => p.CreationTime).ToList();
            var list = _mapper.Map<List<UserDto>>(data);
            return new PageResult<UserDto>(count, list);
        }
    }
}
