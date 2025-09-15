using Common.Application.MediatR.Message;
using Identity.Contrancts.Respon;

namespace Identity.Application.Handler.Get.User;

public class GetUserQuery : IQuery<IEnumerable<UserDto>>
{
    public string UserName { get; set; }
}