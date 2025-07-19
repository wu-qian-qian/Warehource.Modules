using Common.Infrastructure.EF.Repository;
using User.Domain;
using User.Infrastructure.Database;

namespace User.Infrastructure.Role;

internal class RoleRepository : EfCoreRepository<Domain.Role, UserDBContext>, IRoleRepository
{
    public RoleRepository(UserDBContext dbContext) : base(dbContext)
    {
    }
}