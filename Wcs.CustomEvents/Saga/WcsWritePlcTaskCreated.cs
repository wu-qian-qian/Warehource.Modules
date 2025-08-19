namespace Wcs.CustomEvents.Saga;

public record WcsWritePlcTaskCreated(string DeviceName, Dictionary<string, string> DBNameToDataValue, string key);

// 3. 数据集成服务处理结果反馈事件
public record WcsWritePlcTaskCompleted(string DeviceName, bool Success, string key);

// 4. 流程最终状态通知事件
public record WcsWritePlcTaskProcessed(string DeviceName, bool Success, string key);