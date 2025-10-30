using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wcs.Domain.Task;

namespace Wcs.Application.DeviceController
{
    /// <summary>
    /// 公共的基本抽象接口
    /// </summary>
    public interface ICommonController
    {
        /// <summary>
        /// 清除设备任务
        /// </summary>
        /// <param name="deviceName"></param>
        void CleatWcsTaskByDeviceName(string deviceName);

        /// <summary>
        /// 获取设备区域编码
        /// </summary>
        /// <param name="deviceName"></param>
        /// <returns></returns>
        string GetRegionCodesByDeviceName(string deviceName);

        /// <summary>
        /// 获取设备任务
        /// </summary>
        /// <param name="deviceName"></param>
        /// <returns></returns>
        WcsTask? GetWcsTaskByDeviceName(string deviceName);

        /// <summary>
        /// 获取设备任务缓存Key
        /// </summary>
        /// <param name="deviceName"></param>
        /// <returns></returns>
        string GetWcsTaskCacheOfKey(string deviceName);

        void SetWcsTask(string deviceName, WcsTask wcsTask);
    }
}