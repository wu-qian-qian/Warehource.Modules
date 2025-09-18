using Common.Application.MediatR.Message;
using Common.Application.MediatR.Message.PageQuery;
using Common.Shared;
using Identity.Contrancts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Identity.Contrancts.Respon;

namespace Identity.Application.Handler.Page.Role
{
    public class GetRolePageCommand: PageQuery<RoleDto>
    {
        public string? RoleName { get; set; }
    }
}
