using Common.Application.MediatR.Behaviors;
using Common.Application.MediatR.Message;

namespace Wcs.Application.Handler.Http.Complate;

internal class ComplateCommandHandler : ICommandHandler<ComplateCommand, Result<string>>
{
    public Task<Result<string>> Handle(ComplateCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}