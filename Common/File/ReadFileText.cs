using System.Text;

namespace Common.File;

public static class ReadFileText
{
    // <summary>
    /// 读取文件中的固定行（优化版，大文件适用）
    /// </summary>
    /// <param name="filePath">文件路径</param>
    /// <param name="targetLineNumbers">要读取的行号（从 1 开始）</param>
    /// <returns>行号与对应内容的字典</returns>
    public static Dictionary<int, string> ReadFixedLinesLargeFile(string filePath, int[] targetLineNumbers)
    {
        // 验证参数
        if (!System.IO.File.Exists(filePath))
            throw new FileNotFoundException("文件不存在", filePath);
        if (targetLineNumbers == null || targetLineNumbers.Length == 0)
            throw new ArgumentNullException(nameof(targetLineNumbers), "请指定要读取的行号");

        // 处理目标行号：去重、排序（排序后可提前终止遍历）
        var sortedTargetLines = targetLineNumbers.Distinct().OrderBy(lineNum => lineNum).ToList();
        if (sortedTargetLines.Any(lineNum => lineNum < 1))
            throw new ArgumentOutOfRangeException(nameof(targetLineNumbers), "行号必须从 1 开始");

        var result = new Dictionary<int, string>();
        var currentLineNum = 0; // 当前遍历到的行号（从 1 开始）
        var targetIndex = 0; // 当前要匹配的目标行号索引

        // 逐行读取，不加载全部内容到内存
        using (var sr = new StreamReader(filePath, Encoding.UTF8))
        {
            string currentLine;
            while ((currentLine = sr.ReadLine()) != null)
            {
                currentLineNum++;

                // 检查当前行是否是目标行
                if (targetIndex < sortedTargetLines.Count && currentLineNum == sortedTargetLines[targetIndex])
                {
                    result[currentLineNum] = currentLine;
                    targetIndex++;

                    // 所有目标行已找到，提前终止（优化性能）
                    if (targetIndex >= sortedTargetLines.Count)
                        break;
                }

                // 若当前行号已超过最后一个目标行，提前终止（避免无效遍历）
                if (targetIndex < sortedTargetLines.Count && currentLineNum > sortedTargetLines.Last())
                    break;
            }
        }

        // 检查是否有目标行未找到（超出文件总行数）
        foreach (var lineNum in sortedTargetLines.Skip(targetIndex))
            Console.WriteLine($"警告：行号 {lineNum} 超出文件总行数（共 {currentLineNum} 行），已跳过");

        return result;
    }
}