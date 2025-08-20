using Microsoft.AspNetCore.Components.Forms;
using UI.Model;
using UI.Model.Plc;

namespace UI.Service.PlcSevice;

public interface IPlcService
{
    public Task<bool> UpLoadFileAsync(IBrowserFile file);

    public Task<Result<S7NetModel[]>> GetS7NetModesAsync();
    public Task DownLoadFileAsync();
}