using Common.Domain.EF;

namespace Common.Infrastructure.EF.Repository;

public interface IEfCoreRepository<T> where T : IEntity
{
}