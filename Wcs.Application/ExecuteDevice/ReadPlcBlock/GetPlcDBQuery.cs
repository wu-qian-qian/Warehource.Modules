using Common.Application.MediatR.Message;
using Wcs.Device;

namespace Wcs.Application.ExecuteDevice.ReadPlcBlock;

public class GetPlcDBQuery : IQuery<BaseEntity>
{
    public string DeviceName { get; set; }
}