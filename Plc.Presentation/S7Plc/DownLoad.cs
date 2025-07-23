using Common.Helper;
using Common.Presentation.Endpoints;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Plc.Shared;

namespace Plc.Presentation.S7Plc;

public class DownLoad
{
    /// <summary>
    ///     获取文件解析模板
    /// </summary>
    internal class DownLoadS7NetTemplate : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapGet("plc/downLoad-plc-template",
                () =>
                {
                    Dictionary<string, List<object>> dic = new();
                    dic.Add(nameof(S7NetRequest), new List<object>
                    {
                        new S7NetRequest
                        {
                            Ip = "127.0.0.1",
                            Port = 102,
                            Rack = 0,
                            ReadTimeOut = 2000,
                            S7Type = S7TypeEnum.S71200,
                            Solt = 1,
                            WriteTimeOut = 3000
                        }
                    });
                    dic.Add(nameof(S7NetEntityItemRequest), new List<object>
                    {
                        new S7NetEntityItemRequest
                        {
                            Index = 1,
                            S7BlockType = S7BlockTypeEnum.DataBlock,
                            DataOffset = 0,
                            S7DataType = S7DataTypeEnum.Byte,
                            Description = "载货",
                            Ip = "127.0.0.1",
                            Name = "rLoad"
                        }
                    });
                    var excelBytes = ExcelHelper.CreateExcelFromList(dic);
                    return Results.File(
                        excelBytes,
                        "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                        "示例数据.xlsx"
                    );
                }).WithTags(AssemblyReference.Plc);
        }
    }
}