using System.ComponentModel;
using System.Data;
using System.Reflection;
using NPOI.XSSF.UserModel;

namespace Common.Helper;

public static class ExcelHelper
{
    // DataTable 转 Excel
    public static byte[] CreateExcelFromDataTable(DataTable dataTable, string sheetName = "Sheet1")
    {
        using var workbook = new XSSFWorkbook();
        var sheet = workbook.CreateSheet(sheetName);

        // 创建表头行
        var headerRow = sheet.CreateRow(0);
        for (var i = 0; i < dataTable.Columns.Count; i++)
        {
            var cell = headerRow.CreateCell(i);
            cell.SetCellValue(dataTable.Columns[i].ColumnName);
        }

        // 添加数据行
        for (var i = 0; i < dataTable.Rows.Count; i++)
        {
            var row = sheet.CreateRow(i + 1);
            for (var j = 0; j < dataTable.Columns.Count; j++)
            {
                var cell = row.CreateCell(j);
                cell.SetCellValue(dataTable.Rows[i][j]?.ToString() ?? "");
            }
        }

        // 自动调整列宽
        for (var i = 0; i < dataTable.Columns.Count; i++) sheet.AutoSizeColumn(i);

        // 保存到内存流
        using var ms = new MemoryStream();
        workbook.Write(ms);
        return ms.ToArray();
    }

    // 从对象列表创建 Excel
    public static byte[] CreateExcelFromList<T>(List<T> list, string sheetName = "Sheet1")
    {
        using var workbook = new XSSFWorkbook();
        var worksheet = workbook.CreateSheet(sheetName);
        var properties = typeof(T).GetProperties();

        // 添加表头
        var headerRow = worksheet.CreateRow(0);
        for (var i = 0; i < properties.Length; i++)
        {
            var cell = headerRow.CreateCell(i);
            cell.SetCellValue(properties[i].Name);
        }

        // 添加数据行
        for (var i = 0; i < list.Count; i++)
        {
            var row = worksheet.CreateRow(i + 1);
            //列
            for (var j = 0; j < properties.Length; j++)
            {
                var cell = row.CreateCell(j);
                var value = properties[j].GetValue(list[i]);
                cell.SetCellValue(value.ToString() ?? "");
            }
        }

        // 自动调整列宽
        for (var i = 0; i < list.Count; i++) worksheet.AutoSizeColumn(i);

        // 保存到内存流
        using var ms = new MemoryStream();
        workbook.Write(ms);
        return ms.ToArray();
    }

    /// <summary>
    ///     生成一个文件包含多类型的表
    /// </summary>
    /// <param name="dic"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static byte[] CreateExcelFromList(Dictionary<string, List<object>> dic)
    {
        using var workbook = new XSSFWorkbook();
        foreach (var sheet in dic)
        {
            var worksheet = workbook.CreateSheet(sheet.Key);
            var properties = sheet.Value[0].GetType().GetProperties();

            // 添加表头
            var headerRow = worksheet.CreateRow(0);
            for (var i = 0; i < properties.Length; i++)
            {
                var cell = headerRow.CreateCell(i);
                var name = properties[i].GetCustomAttribute<DescriptionAttribute>()?.Description ?? properties[i].Name;
                cell.SetCellValue(name);
            }

            // 添加数据行
            for (var i = 0; i < sheet.Value.Count; i++)
            {
                var row = worksheet.CreateRow(i + 1);
                //列
                for (var j = 0; j < properties.Length; j++)
                {
                    var cell = row.CreateCell(j);
                    var value = properties[j].GetValue(sheet.Value[i]);
                    cell.SetCellValue(value?.ToString() ?? "");
                }
            }

            // 自动调整列宽
            for (var i = 0; i < sheet.Value.Count; i++) worksheet.AutoSizeColumn(i);
        }

        using var ms = new MemoryStream();
        workbook.Write(ms);
        return ms.ToArray();
    }

    // 将 Excel 字节数组保存为文件
    public static void SaveExcelFile(byte[] excelData, string filePath)
    {
        using var stream = new FileStream(filePath, FileMode.Create, FileAccess.Write);
        stream.Write(excelData, 0, excelData.Length);
    }
}