using UI.Model;
using UI.Model.Identity;

namespace UI.Service.IdentityService;

public interface IIdentityService
{
    Task<Result<IdentityUserModel[]>> GetUsersAsync();

    Task<Result<IdentityRoleModel[]>> GetRolesAsync();

    Task<Result<IdentityUserModel>> CreateUserAsync(IdentityUserModel user);

    Task<Result<IdentityRoleModel>> CreateRoleAsync(IdentityRoleModel role);
}