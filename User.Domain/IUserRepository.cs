using Common.Domain.EF;

namespace Identity.Domain;

public interface IUserRepository : IRepository<User>
{
    public Task<User> GetUserAndRoleAsync(string userName);

    public Task<List<User>> GetAllUserAndRoleAsync();

    public IQueryable<User> GetQueryable();
}