using UI.Model.Identity;

namespace UI.Service.IdentityService;

public interface IIdentityService
{
    Task<IdentityUserModel[]> GetUsersAsync();

    Task<IdentityRoleModel[]> GetRolesAsync();

    Task CreateUserAsync(IdentityUserModel user);

    Task CreateRoleAsync(IdentityRoleModel role);
}