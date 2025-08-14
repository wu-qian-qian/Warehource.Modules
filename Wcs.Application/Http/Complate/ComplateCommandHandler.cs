using Common.Application.MediatR.Behaviors;
using Common.Application.MediatR.Message;

namespace Wcs.Application.Http.Complate;

internal class ComplateCommandHandler : ICommandHandler<ComplateCommandEvent, Result<string>>
{
    public Task<Result<string>> Handle(ComplateCommandEvent request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}