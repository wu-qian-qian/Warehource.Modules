using Common.Application.MediatR.Behaviors;
using Common.Application.MediatR.Message;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wcs.Domain.Task;
using Wcs.Shared;

namespace Wcs.Application.Handler.Business.GetExecuteTask
{
    internal class GetExecuteTaskQuery : IQuery<WcsTask[]>
    {
        /// <summary>
        /// 设备名称
        /// </summary>
        public string DeviceName { get; set; } = "Device";

        /// <summary>
        /// 设备类型
        /// </summary>
        public DeviceTypeEnum DeviceType { get; set; }

        /// <summary>
        /// 任务缓存Key
        /// </summary>
        public string TaskCacheKey { get; set; }

        /// <summary>
        /// 区域编码集合
        /// </summary>
        public string Region { get; set; }

        /// <summary>
        /// 任务类型
        /// </summary>
        public WcsTaskTypeEnum? WcsTaskType { get; set; }

        /// <summary>
        /// 下位机任务号 上位机流水号
        /// </summary>
        public int? SerialNumber { get; set; }

        /// <summary>
        /// 取货巷道
        /// </summary>
        public string? GetTunnle { get; set; }

        /// <summary>
        /// 放货巷道
        /// </summary>
        public string? PutTunnle { get; set; }

        /// <summary>
        /// 容器编码
        /// </summary>
        public string? Container { get; set; }

        /// <summary>
        /// 任务状态
        /// </summary>
        public WcsTaskStatusEnum? TaskStatus { get; set; }
    }
}