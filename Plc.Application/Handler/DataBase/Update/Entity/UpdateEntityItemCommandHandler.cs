using Common.Application.MediatR.Behaviors;
using Common.Application.MediatR.Message;
using Plc.Application.Abstract;
using Plc.Domain.S7;

namespace Plc.Application.Handler.DataBase.Update.Entity;

internal class UpdateEntityItemCommandHandler(IS7NetManager _netManager, IUnitOfWork _unitOfWork)
    : ICommandHandler<UpateEntityItemCommand, Result<bool>>
{
    public async Task<Result<bool>> Handle(UpateEntityItemCommand request, CancellationToken cancellationToken)
    {
        var result = new Result<bool>();
        var entityItem = await _netManager.GetEntityItemAsync(request.Id);
        entityItem.BitOffset = request.BitOffset;
        entityItem.Name = request.Name;
        entityItem.Description = request.Description;
        entityItem.DeviceName = request.DeviceName;
        entityItem.DataOffset = request.DataOffset;
        entityItem.ArrayLength = request.ArrayLength;
        entityItem.S7BlockType = request.S7BlockType;
        entityItem.S7DataType = request.S7DataType;
        entityItem.IsUse = request.IsUse;
        entityItem.Index = request.Index;
        entityItem.DBAddress = request.DBAddress;
        result.SetValue(true);
        await _unitOfWork.SaveChangesAsync();
        return result;
    }
}