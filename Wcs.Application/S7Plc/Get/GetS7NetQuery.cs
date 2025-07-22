using Common.Application.MediatR.Message;
using Wcs.Domain.S7;

namespace Wcs.Application.S7Plc.Get;

public class GetS7NetQuery : IQuery<IEnumerable<S7NetConfig>>
{
}