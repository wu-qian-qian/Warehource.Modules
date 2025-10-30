namespace Wcs.Shared;

/// <summary>
/// 自定义状态机状态步骤
/// </summary>
public enum TaskExecuteStepTypeEnum
{
    Pause = -1,

    /// <summary>
    ///   状态机默认状态
    /// </summary>
    None = 0,

    /// <summary>
    /// 发送任务
    /// </summary>
    SendPlc = 1,

    /// <summary>
    ///     用来描述发送任务
    /// </summary>
    BeSendingPlc = 2,

    /// <summary>
    ///     一般用来描述结束
    /// </summary>
    SendEndingPlc = 3,

    /// <summary>
    ///     状态机最终状态
    /// </summary>
    Complate = 99,

    /// <summary>
    /// 自定义申请通道状态机
    /// </summary>
    ApplyTunnle = 4,

    /// <summary>
    /// 限流状态机
    /// </summary>
    Limted = 5,

    /// <summary>
    /// 获取WCS任务状态机
    /// </summary>
    GetWcsTask = 6,

    /// <summary>
    /// 申请库位状态机
    /// </summary>
    ApplyLocation = 7,

    /// <summary>
    /// 获取站台点位状态机
    /// </summary>
    GetStationPoint = 8,
}