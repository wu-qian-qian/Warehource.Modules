using Common.Application.MediatR.Message;
using Plc.Contracts.S7Plc;

namespace Plc.Application.S7Plc.Insert;

public class InsertS7NetConfigCommandHandler : ICommandHandler<InsertS7NetConfigCommand, S7NetDto>
{
    public Task<S7NetDto> Handle(InsertS7NetConfigCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}