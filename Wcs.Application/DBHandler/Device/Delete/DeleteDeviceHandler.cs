using Common.Application.MediatR.Behaviors;
using Common.Application.MediatR.Message;
using Wcs.Application.Abstract;
using Wcs.Domain.Device;

namespace Wcs.Application.DBHandler.Device.Delete;

public class DeleteDeviceHandler(IDeviceRepository _deviceRepository, IUnitOfWork _unitOfWork)
    : ICommandHandler<DeleteDeviceEvent, Result<string>>
{
    public async Task<Result<string>> Handle(DeleteDeviceEvent request, CancellationToken cancellationToken)
    {
        var result = new Result<string>();
        var entity = _deviceRepository.Get(request.Id);
        if (entity == null)
        {
            result.SetMessage("不存在节点");
        }
        else
        {
            result.SetValue("删除成功");
            await _unitOfWork.SaveChangesAsync();
        }

        return result;
    }
}