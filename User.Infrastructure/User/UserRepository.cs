using Common.Infrastructure.EF;
using Common.Infrastructure.EF.Repository;
using Microsoft.EntityFrameworkCore;
using User.Domain;
using User.Infrastructure.Database;

namespace User.Infrastructure.User;

internal class UserRepository : EfCoreRepository<Domain.User, UserDBContext>, IUserRepository
{
    public UserRepository(UserDBContext dbContext) : base(dbContext)
    {
    }

    public Task<Domain.User> GetUserAndRoleAsync(string userName)
    {
        return dbContext.Query<Domain.User>().Include(p => p.Role).FirstAsync(p => p.Username == userName);
    }

    public Task<List<Domain.User>> GetAllUserAndRoleAsync()
    {
        return dbContext.Query<Domain.User>().Include(p => p.Role).ToListAsync();
    }
}