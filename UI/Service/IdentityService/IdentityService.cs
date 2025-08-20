using UI.Model;
using UI.Model.Identity;

namespace UI.Service.IdentityService;

public class IdentityService : IIdentityService
{
    private readonly IHttpClientFactory _httpClientFactory;

    public IdentityService(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public Task<Result<IdentityUserModel[]>> GetUsersAsync()
    {
        Result<IdentityUserModel[]> result = new();
        var data = Enumerable.Range(0, 20).Select(
            p => new IdentityUserModel
            {
                Name = $"User{p}",
                Description = "This is a test user",
                Username = "user" + p,
                Phone = "0123456789",
                Email = "user" + p
            });
        result.Value = data.ToArray();
        return Task.FromResult(result);
    }

    public Task<Result<IdentityRoleModel[]>> GetRolesAsync()
    {
        Result<IdentityRoleModel[]> result = new();
        var data = Enumerable.Range(0, 20).Select(
            p => new IdentityRoleModel
            {
                RoleName = $"Role{p}",
                Description = "This is a test user"
            });
        result.Value = data.ToArray();
        return Task.FromResult(result);
    }

    public Task<Result<IdentityUserModel>> CreateUserAsync(IdentityUserModel user)
    {
        throw new NotImplementedException();
    }

    public Task<Result<IdentityRoleModel>> CreateRoleAsync(IdentityRoleModel role)
    {
        throw new NotImplementedException();
    }
}