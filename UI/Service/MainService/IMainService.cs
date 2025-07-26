using UI.Model.Main;

namespace UI.Service.MainService;

public interface IMainService
{
    public Task<UserModel> LoginAsync(string username, string password);
    public Task<List<MenuItemData>> GetMenuItemDatas(string? role);
}