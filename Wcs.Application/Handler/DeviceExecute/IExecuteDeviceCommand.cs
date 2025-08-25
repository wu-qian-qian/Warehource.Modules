using Common.Application.MediatR.Message;
using Wcs.Device.Abstract;
using Wcs.Shared;

namespace Wcs.Application.Handler.DeviceExecute;

public abstract class IExecuteDeviceCommand : ICommand
{
    protected IExecuteDeviceCommand()
    {
    }

    /// <summary>
    ///     推荐使用
    ///     dbEntity 使用的是对象传递
    /// </summary>
    /// <param name="name"></param>
    /// <param name="dbKey"></param>
    /// <param name="deviceType"></param>
    /// <param name="dbEntity"></param>
    protected IExecuteDeviceCommand(string name, string dbKey, DeviceTypeEnum deviceType, BaseDBEntity dbEntity)
    {
        ReadPlc = new ReadPlc(name, dbKey, deviceType, dbEntity);
    }


    internal ReadPlc ReadPlc { get; init; }
}

internal record ReadPlc(string DeviceName, string Key, DeviceTypeEnum DeviceType, BaseDBEntity DBEntity);