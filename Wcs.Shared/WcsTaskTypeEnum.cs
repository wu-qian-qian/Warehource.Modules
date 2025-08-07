namespace Wcs.Shared;


/// <summary>
/// 使用管道进行任务找寻，管道中判断任务类型
/// </summary>
public enum WcsTaskTypeEnum
{
    StockIn = 1, // 入库
    StockOut = 2, // 出库
    StockMove = 3 // 移库
}