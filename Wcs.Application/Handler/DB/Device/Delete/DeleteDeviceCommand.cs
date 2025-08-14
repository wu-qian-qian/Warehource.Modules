using Common.Application.MediatR.Behaviors;
using Common.Application.MediatR.Message;

namespace Wcs.Application.Handler.DB.Device.Delete;

public class DeleteDeviceCommand : ICommand<Result<string>>
{
    public Guid Id { get; set; }
}