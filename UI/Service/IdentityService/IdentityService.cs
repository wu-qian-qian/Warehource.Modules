using UI.Model.Identity;

namespace UI.Service.IdentityService;

public class IdentityService : IIdentityService
{
    private readonly IHttpClientFactory _httpClientFactory;

    public IdentityService(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public Task<IdentityUserModel[]> GetUsersAsync()
    {
        var data = Enumerable.Range(0, 20).Select(
            p => new IdentityUserModel
            {
                Name = $"User{p}",
                Description = "This is a test user",
                Username = "user" + p,
                Phone = "0123456789",
                Email = "user" + p
            });
        return Task.FromResult(data.ToArray());
    }

    public Task<IdentityRoleModel[]> GetRolesAsync()
    {
        var data = Enumerable.Range(0, 20).Select(
            p => new IdentityRoleModel
            {
                RoleName = $"Role{p}",
                Description = "This is a test user"
            });
        return Task.FromResult(data.ToArray());
    }

    public Task CreateUserAsync(IdentityUserModel user)
    {
        throw new NotImplementedException();
    }

    public Task CreateRoleAsync(IdentityRoleModel role)
    {
        throw new NotImplementedException();
    }
}