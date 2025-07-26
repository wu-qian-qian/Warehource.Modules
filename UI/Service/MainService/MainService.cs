using UI.Model.Main;

namespace UI.Service.MainService;

public class MainService : IMainService
{
    private readonly IHttpClientFactory _httpFactory;

    public MainService(IHttpClientFactory httpFactory)
    {
        _httpFactory = httpFactory;
    }

    public Task<UserModel> LoginAsync(string username, string password)
    {
        return Task.FromResult(new UserModel
        {
            Name = "管理员",
            RoleName = "admin",
            IsLogin = true
        });
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
            new()
            {
                Key = "1",
                Title = "首页",
                Href = "/",
                Children = null
            }
        };

        if (role == "admin")
        {
            menuItems.Add(new MenuItemData
            {
                Key = "2",
                Title = "Wcs配置",
                Children = new List<MenuItemData>
                {
                    new() { Key = "2-1", Title = "后台任务配置", Href = "JobConfig" },
                    new() { Key = "2-2", Title = "Plc数据配置" },
                    new() { Key = "2-3", Title = "Wcs执行步骤配置" },
                    new() { Key = "2-4", Title = "Wcs设备执行配置" }
                }
            });
            menuItems.Add(new MenuItemData
            {
                Key = "3",
                Title = "用户管理",
                Children = new List<MenuItemData>
                {
                    new() { Key = "3-1", Title = "用户列表" },
                    new() { Key = "3-2", Title = "角色列表" }
                }
            });
            menuItems.Add(new MenuItemData
            {
                Key = "4",
                Title = "系统设置",
                Children = new List<MenuItemData>
                {
                    new() { Key = "4-1", Title = "系统配置" },
                    new() { Key = "4-2", Title = "日志管理" }
                }
            });
        }

        return Task.FromResult(menuItems);
        //throw new NotImplementedException();
    }
}