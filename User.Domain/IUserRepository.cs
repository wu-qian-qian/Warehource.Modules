using Common.Domain.EF;

namespace User.Domain;

public interface IUserRepository : IRepository<User>
{
    public Task<User> GetUserAndRoleAsync(string userName);

    public Task<List<User>> GetAllUserAndRoleAsync();
}