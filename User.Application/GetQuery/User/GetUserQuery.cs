using Common.Application.MediatR.Message;
using Identity.Contrancts;

namespace Identity.Application.GetQuery;

public class GetUserQuery : IQuery<IEnumerable<UserDto>>
{
}