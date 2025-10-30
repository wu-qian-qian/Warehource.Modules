namespace Wcs.CustomEvents.Saga;

/// <summary>
/// </summary>
/// <param name="DeviceName"></param>
/// <param name="DBNameToDataValue"></param>
/// <param name="Key">设备任务数据的唯一编码</param>
public record WcsWritePlcTaskCreated(string DeviceName, Dictionary<string, string> DBNameToDataValue, string Key);

// 3. 数据集成服务处理结果反馈事件
public record WcsWritePlcTaskResult(string DeviceName, bool Success, string Key);

// 4. 流程最终状态通知事件
public record WcsWritePlcTaskProcessed(string DeviceName, bool Success, string Key);