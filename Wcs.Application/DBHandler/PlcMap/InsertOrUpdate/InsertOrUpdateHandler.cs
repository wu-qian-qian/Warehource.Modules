using Common.Application.MediatR.Message;
using Wcs.Application.Abstract;
using Wcs.Domain.Plc;

namespace Wcs.Application.DBHandler.PlcMap.InsertOrUpdate;

internal class InsertOrUpdateHandler(IUnitOfWork _unitOfWork, IPlcMapRepository _plcMapRepository)
    : ICommandHandler<InsertOrUpdateEvent>
{
    public async Task Handle(InsertOrUpdateEvent request, CancellationToken cancellationToken)
    {
        if (request.UpdateList == null || request.UpdateList.Length <= 0)
        {
            var plcMap = request.PlcEntityName.Select(p =>
            {
                Domain.Plc.PlcMap item = new();
                item.PlcName = p;
                item.DeviceName = request.DeviceName;
                return item;
            }).ToArray();
            _plcMapRepository.Insert(plcMap);
        }
        else
        {
            var updateList = _plcMapRepository.GetPlcMapOfDeviceName(request.DeviceName).ToArray();
            for (var i = 0; i < request.UpdateList.Length; i++)
                if (updateList.Any(p => p.PlcName == request.UpdateList[i].Key))
                {
                    var item = updateList.First(p => p.PlcName == request.UpdateList[i].Key);
                    item.PlcName = request.UpdateList[i].Value;
                }

            _plcMapRepository.Update(updateList);
        }

        await _unitOfWork.SaveChangesAsync();
    }
}