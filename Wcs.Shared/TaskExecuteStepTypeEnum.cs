namespace Wcs.Shared;

public enum TaskExecuteStepTypeEnum
{
    /// <summary>
    ///     一般用来更新一些任务数据
    /// </summary>
    ToBeSend = 0,

    /// <summary>
    ///     用来描述发送任务
    /// </summary>
    BeSending = 1,

    /// <summary>
    ///     一般用来描述结束
    /// </summary>
    SendEnding = 2,

    /// <summary>
    ///     一般用来描述成功
    /// </summary>
    Complate = 3
}