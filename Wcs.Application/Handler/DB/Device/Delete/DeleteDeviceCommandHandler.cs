using Common.Application.MediatR.Behaviors;
using Common.Application.MediatR.Message;
using Wcs.Application.Abstract;
using Wcs.Domain.Device;

namespace Wcs.Application.Handler.DB.Device.Delete;

public class DeleteDeviceCommandHandler(IDeviceRepository _deviceRepository, IUnitOfWork _unitOfWork)
    : ICommandHandler<DeleteDeviceCommand, Result<string>>
{
    public async Task<Result<string>> Handle(DeleteDeviceCommand request, CancellationToken cancellationToken)
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