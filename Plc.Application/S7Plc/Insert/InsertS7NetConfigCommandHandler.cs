using Common.Application.MediatR.Message;
using Plc.Application.Abstract;
using Plc.Contracts.Respon;
using Plc.Domain.S7;

namespace Plc.Application.S7Plc.Insert;

/// <summary>
///     excel导入只能一块插入
/// </summary>
public class InsertS7NetConfigCommandHandler(IS7NetManager netManager, IUnitOfWork unitOfWork)
    : ICommandHandler<InsertS7NetConfigCommand, S7NetDto>
{
    public Task<S7NetDto> Handle(InsertS7NetConfigCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}