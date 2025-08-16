using Common.Infrastructure.EF;
using Common.Infrastructure.EF.Repository;
using Identity.Domain;
using Identity.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Identity.Infrastructure.User;

internal class UserRepository : EfCoreRepository<Domain.User, UserDBContext>, IUserRepository
{
    public UserRepository(UserDBContext dbContext) : base(dbContext)
    {
    }

    public Task<Domain.User> GetUserAndRoleAsync(string userName)
    {
        return DbContext.Query<Domain.User>().Include(p => p.Role).FirstOrDefaultAsync(p => p.Username == userName);
    }

    public Task<List<Domain.User>> GetAllUserAndRoleAsync()
    {
        return DbContext.Query<Domain.User>().Include(p => p.Role).ToListAsync();
    }
}