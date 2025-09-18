using Common.Application.MediatR.Message.PageQuery;
using Common.Shared;
using Identity.Contrancts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Identity.Contrancts.Respon;

namespace Identity.Application.Handler.Page.User
{
    public class GetUserPageCommand : PageQuery<UserDto>
    {
        public string? UserName { get; set; }
    }
}
