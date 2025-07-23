using Common.Application.MediatR.Message;
using Plc.Contracts.S7Plc;

namespace Plc.Application.S7Plc.Insert;

public class InsertS7EntityItemCommandHandler : ICommandHandler<InsertS7EntityItemCommand, S7EntityItemDto>
{
    public Task<S7EntityItemDto> Handle(InsertS7EntityItemCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}