using Common.Application.MediatR.Message.PageQuery;
using Identity.Contrancts.Respon;

namespace Identity.Application.Handler.Page.User;

public class GetUserPageCommand : PageQuery<UserDto>
{
    public string? UserName { get; set; }
}