namespace Wcs.Shared;

public enum WcsTaskStatusEnum
{
    Created = 1, // 创建
    Analysited = 2, //解析
    InProgress = 3, // 进行中
    Completed = 4, // 完成
    Failed = 5, // 失败
    Cancelled = 6 // 已取消
}