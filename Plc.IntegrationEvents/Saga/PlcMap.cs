namespace Plc.CustomEvents.Saga;

public class PlcMap
{
    // 1. 启动事件：触发Saga流程
    public record PlcMapCreated(string DeviceName, string[] EntityNames);


    // 3. 数据集成服务处理结果反馈事件
    public record PlcMapDataIntegrationCompleted(string DeviceName, bool Success);

    // 4. 流程最终状态通知事件
    public record PlcMapProcessed(string DeviceName, bool Success);
}