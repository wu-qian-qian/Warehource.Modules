using Common.Infrastructure.EF.Repository;
using User.Domain;
using User.Infrastructure.Database;

namespace User.Infrastructure.User;

internal class UserRepository : EfCoreRepository<Domain.User, UserDBContext>, IUserRepository
{
    public UserRepository(UserDBContext dbContext) : base(dbContext)
    {
    }
}