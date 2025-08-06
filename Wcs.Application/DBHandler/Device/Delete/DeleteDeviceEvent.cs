using Common.Application.MediatR.Behaviors;
using Common.Application.MediatR.Message;

namespace Wcs.Application.DBHandler.Device.Delete;

public class DeleteDeviceEvent : ICommand<Result<string>>
{
    public Guid Id { get; set; }
}