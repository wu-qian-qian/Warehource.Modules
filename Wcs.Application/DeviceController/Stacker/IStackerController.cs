using Wcs.CustomEvents.Saga;
using Wcs.Device.Abstract;
using Wcs.Device.DeviceStructure.Stacker;

namespace Wcs.Application.DeviceController.Stacker;

/// <summary>
///     堆垛机调度中心
/// </summary>
public interface IStackerController
    : IController<AbstractStacker>, ICommonController
{
    /// <summary>
    /// 是否为堆垛机的入库点位
    /// 入库接货口
    /// </summary>
    /// <param name="deviceName"></param>
    /// <returns></returns>
    bool IsTranshipPointByDeviceName(string deviceName);

    /// <summary>
    /// 获取堆垛机所属隧道号
    /// </summary>
    /// <param name="deviceName"></param>
    /// <returns></returns>
    string GetTunnleByDeviceName(string deviceName);

    /// <summary>
    /// 获取写入PLC任务数据
    /// </summary>
    /// <param name="deviceName"></param>
    /// <returns></returns>
    WcsWritePlcTaskCreated TryGetWritePlcTaskData(string deviceName);

    /// <summary>
    /// 设备任务是否完成
    /// </summary>
    /// <param name="deviceName"></param>
    /// <returns></returns>
    bool IsComplateByDeviceName(string deviceName);
}