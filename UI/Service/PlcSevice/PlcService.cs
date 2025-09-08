using System.Net.Http.Headers;
using Microsoft.AspNetCore.Components.Forms;
using UI.Model;
using UI.Model.Plc;
using UI.Service.MainService;

namespace UI.Service.PlcSevice;

public class PlcService : IPlcService
{
    private readonly IHttpClientFactory _httpFactory;

    public PlcService(IHttpClientFactory httpFactory, IMainService service)
    {
        _httpFactory = httpFactory;
    }


    public async Task<bool> UpLoadFileAsync(IBrowserFile file)
    {
        var httpClient = _httpFactory.CreateClient();
        try
        {
            using (var formData = new MultipartFormDataContent())
            {
                var stream = file.OpenReadStream();
                // 读取文件内容
                var fileContent = new StreamContent(stream);

                // 设置文件内容类型
                fileContent.Headers.ContentType =
                    new MediaTypeHeaderValue("application/octet-stream");

                // 添加文件到表单数据，"file"是服务器端接收的参数名
                formData.Add(fileContent, "file", Path.GetFileName(file.Name));
                // 可以添加其他表单字段
                // formData.Add(new StringContent("additional data"), "fieldName");
                // 发送POST请求上传文件
                var response = await httpClient.PostAsync("http://localhost:5050/plc/add-allplc-config", formData);
                // 确保响应成功
                response.EnsureSuccessStatusCode();
            }

            return true;
        }
        catch (Exception e)
        {
            //log
            return false;
        }
    }

    public async Task DownLoadFileAsync()
    {
        var httpClient = _httpFactory.CreateClient();
        var response = await httpClient.GetAsync("http://localhost:5050/plc/downLoad-plc-template");
    }

    public Task<Result<S7NetModel[]>> GetS7NetModesAsync()
    {
        Result<S7NetModel[]> result = new();
        var data = new List<S7NetModel>
        {
            new()
            {
                Ip = "127.0.0.1",
                Port = 102,
                Solt = 0,
                Rack = 1,
                s7EntityItemModels = new List<S7EntityItemModel>
                {
                    new()
                    {
                        Index = 1,
                        Description = "超时",
                        DeviceName = "输送",
                        DBAddress = 1,
                        S7BlockType = "db",
                        S7DataType = "int",
                        Name = "TimeOut"
                    },
                    new()
                    {
                        Index = 1,
                        Description = "超时",
                        DeviceName = "输送",
                        DBAddress = 1,
                        S7BlockType = "db",
                        S7DataType = "int",
                        Name = "TimeOut"
                    }
                }.ToArray()
            },
            new()
            {
                Ip = "127.0.0.1",
                Port = 102,
                Solt = 0,
                Rack = 1,
                s7EntityItemModels = new List<S7EntityItemModel>
                {
                    new()
                    {
                        Index = 1,
                        Description = "超时",
                        DeviceName = "输送",
                        DBAddress = 1,
                        S7BlockType = "db",
                        S7DataType = "int",
                        Name = "TimeOut"
                    },
                    new()
                    {
                        Index = 1,
                        Description = "超时",
                        DeviceName = "输送",
                        DBAddress = 1,
                        S7BlockType = "db",
                        S7DataType = "int",
                        Name = "TimeOut"
                    }
                }.ToArray()
            },
            new()
            {
                Ip = "127.0.0.1",
                Port = 102,
                Solt = 0,
                Rack = 1,
                s7EntityItemModels = new List<S7EntityItemModel>
                {
                    new()
                    {
                        Index = 1,
                        Description = "超时",
                        DeviceName = "输送",
                        DBAddress = 1,
                        S7BlockType = "db",
                        S7DataType = "int",
                        Name = "TimeOut"
                    },
                    new()
                    {
                        Index = 1,
                        Description = "超时",
                        DeviceName = "输送",
                        DBAddress = 1,
                        S7BlockType = "db",
                        S7DataType = "int",
                        Name = "TimeOut"
                    }
                }.ToArray()
            },
            new()
            {
                Ip = "127.0.0.1",
                Port = 102,
                Solt = 0,
                Rack = 1,
                s7EntityItemModels = new List<S7EntityItemModel>
                {
                    new()
                    {
                        Index = 1,
                        Description = "超时",
                        DeviceName = "输送",
                        DBAddress = 1,
                        S7BlockType = "db",
                        S7DataType = "int",
                        Name = "TimeOut"
                    },
                    new()
                    {
                        Index = 1,
                        Description = "超时",
                        DeviceName = "输送",
                        DBAddress = 1,
                        S7BlockType = "db",
                        S7DataType = "int",
                        Name = "TimeOut"
                    }
                }.ToArray()
            }
        }.ToArray();
        result.Value = data;
        return Task.FromResult(result);
    }
}