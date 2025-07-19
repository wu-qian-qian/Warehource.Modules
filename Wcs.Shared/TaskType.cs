namespace Wcs.Shared;

public enum WcsTaskTypeEnum
{
    StockIn = 1, // 入库
    StockOut = 2, // 出库
    StockMove = 3 // 移库
}

public enum WcsTaskStatusEnum
{
    Created = 1, // 创建
    InProgress = 2, // 进行中
    Completed = 3, // 完成
    Failed = 4, // 失败
    Cancelled = 5 // 已取消
}