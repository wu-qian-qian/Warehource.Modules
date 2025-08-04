using Microsoft.AspNetCore.Components.Forms;
using UI.Model.Plc;

namespace UI.Service.PlcSevice;

public interface IPlcService
{
    public Task<bool> UpLoadFileAsync(IBrowserFile file);

    public Task<S7NetModel[]> GetS7NetModesAsync();
    public Task DownLoadFileAsync();
}