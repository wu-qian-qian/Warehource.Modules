using Common.Application.MediatR.Message;
using Identity.Contrancts;

namespace Identity.Application.GetQuery.User;

public class GetUserQuery : IQuery<IEnumerable<UserDto>>
{
}