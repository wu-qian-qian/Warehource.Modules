using UI.Model;
using UI.Model.Main;

namespace UI.Service.MainService;

public class MainService : IMainService
{
    private readonly IHttpClientFactory _httpFactory;


    public MainService(IHttpClientFactory httpFactory)
    {
        _httpFactory = httpFactory;
    }

    public string Token { get; private set; }

    public Task<string> GetToken()
    {
        return Task.FromResult(Token);
    }

    public Task<Result<UserModel>> LoginAsync(string username, string password)
    {
        Result<UserModel> result = new();
        result.Value = new UserModel
        {
            Name = "管理员",
            RoleName = "admin",
            IsLogin = true
        };
        Token = DateTime.Now.ToString();
        return Task.FromResult(result);
    }

    /// <summary>
    ///     数据库 or json
    /// </summary>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public Task<List<MenuItemData>> GetMenuItemDatas(string? role)
    {
        var menuItems = new List<MenuItemData>
        {
        };

        if (role == "admin")
        {
            menuItems.Add(new MenuItemData
            {
                Key = "1",
                Title = "首页",
                Children = null,
                Href = "WcsTask"
            });
            menuItems.Add(new MenuItemData
            {
                Key = "2",
                Title = "Wcs配置",
                Children = new List<MenuItemData>
                {
                    new() { Key = "2-1", Title = "后台任务配置", Href = "JobConfig" },
                    new() { Key = "2-2", Title = "Plc数据配置", Href = "PlcConfig" },
                    new() { Key = "2-3", Title = "Wcs执行步骤配置", Href = "ExecuteNodePath" },
                    new() { Key = "2-4", Title = "区域列表", Href = "RegionConfig" },
                    new() { Key = "2-5", Title = "设备列表", Href = "DeviceConfig" }
                }
            });
            menuItems.Add(new MenuItemData
            {
                Key = "3",
                Title = "用户管理",
                Children = new List<MenuItemData>
                {
                    new() { Key = "3-1", Title = "用户列表", Href = "UserConfig" },
                    new() { Key = "3-2", Title = "角色列表", Href = "RoleConfig" }
                }
            });
            menuItems.Add(new MenuItemData
            {
                Key = "4",
                Title = "执行设备状态",
                Children = new List<MenuItemData>
                {
                    new() { Key = "4-1", Title = "堆垛机", Href = "Stacker" },
                    new() { Key = "4-2", Title = "出入库口", Href = "StockPort" }
                }
            });
            menuItems.Add(new MenuItemData
            {
                Key = "5",
                Title = "系统设置",
                Children = new List<MenuItemData>
                {
                    new() { Key = "5-1", Title = "系统配置" },
                    new() { Key = "5-2", Title = "日志管理" }
                }
            });
        }

        return Task.FromResult(menuItems);
        //throw new NotImplementedException();
    }
}