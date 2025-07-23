using System.ComponentModel;

namespace Plc.Contracts.S7Plc;

public sealed class PlcPipeLineSignalDto
{
    [Description("载货")] public string RLoaded { get; set; }

    [Description("就绪")] public string RStatus { get; set; }

    [Description("模式")] public string RMode { get; set; }

    [Description("任务号")] public string RTaskNo { get; set; }

    [Description("完成任务号")] public string RFinshTaskNo { get; set; }

    [Description("写入任务号")] public string WTaskNo { get; set; }

    [Description("写目标位")] public string WTarget { get; set; }
}