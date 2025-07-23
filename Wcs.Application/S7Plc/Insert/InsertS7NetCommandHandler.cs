using Common.Application.MediatR.Message;
using Wcs.Contracts.S7Plc;

namespace Wcs.Application.S7Plc.Insert;

public class InsertS7NetConfigCommandHandler:ICommandHandler<InsertS7NetConfigCommand,S7NetDto>
{
    public Task<S7NetDto> Handle(InsertS7NetConfigCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}

public class InsertS7EntityItemCommandHandler:ICommandHandler<InsertS7EntityItemCommand,S7EntityItemDto>
{
    public Task<S7EntityItemDto> Handle(InsertS7EntityItemCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}