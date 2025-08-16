using System.ComponentModel;
using System.Reflection;
using NPOI.XSSF.UserModel;

namespace Common.Helper;

public static class ExcelHelper
{
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


    public static Dictionary<string, List<object>> CreateObjectFromList(Stream stream, List<Type> types)
    {
        using var workbook = new XSSFWorkbook(stream);
        var dic = new Dictionary<string, List<object>>();
        foreach (var type in types)
        {
            var worksheet = string.IsNullOrEmpty(type.Name) ? workbook.GetSheetAt(0) : workbook.GetSheet(type.Name);
            var properties = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);
            var headerRow = worksheet.GetRow(0);
            var dataList = new List<object>();
            for (var rowIndex = 1; rowIndex <= worksheet.LastRowNum; rowIndex++)
            {
                var ins = Activator.CreateInstance(type);
                var rowData = worksheet.GetRow(rowIndex);
                for (var i = 0; i < headerRow.LastCellNum; i++)
                {
                    var headCell = headerRow.GetCell(i);
                    var propertie = properties
                        .FirstOrDefault(p => p.Name == headCell.ToString().Trim());

                    //列值
                    var cellData = rowData.GetCell(i)?.ToString()?.Trim();
                    if (cellData == string.Empty || cellData == null)
                        continue;
                    //属性
                    var datatype = propertie.PropertyType;
                    if (datatype.IsEnum)
                    {
                        // 如果是枚举类型，尝试转换
                        var enumValue = Enum.Parse(datatype, cellData, true);
                        propertie.SetValue(ins, enumValue);
                        continue;
                    }

                    propertie.SetValue(ins, TypeHelper.NullableSetValue(datatype, cellData));
                }

                dataList.Add(ins);
            }

            dic.Add(type.Name, dataList);
        }

        return dic;
    }


    // 将 Excel 字节数组保存为文件
    public static void SaveExcelFile(byte[] excelData, string filePath)
    {
        using var stream = new FileStream(filePath, FileMode.Create, FileAccess.Write);
        stream.Write(excelData, 0, excelData.Length);
    }
}