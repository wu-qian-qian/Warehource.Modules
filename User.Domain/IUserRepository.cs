using Common.Domain.EF;

namespace User.Domain;

public interface IUserRepository : IRepository<User>
{
    public Task<Domain.User> GetUserAndRoleAsync(string userName);

    public Task<List<Domain.User>> GetAllUserAndRoleAsync();
}