using Common.Application.MediatR.Message;
using Plc.Application.Abstract;
using Plc.Domain.S7;

namespace Plc.Application.Handler.DataBase.Delete;

internal class DeletePlcCommandHandler(IS7NetManager _netManager, IUnitOfWork _unitOfWork)
    : ICommandHandler<DeletePlcCommand>
{
    public async Task Handle(DeletePlcCommand request, CancellationToken cancellationToken)
    {
        var s7NetConfig = await _netManager.GetNetWiteIdAsync(request.Id);
        await _netManager.DeleteAsync([s7NetConfig]);
        await _unitOfWork.SaveChangesAsync();
    }
}