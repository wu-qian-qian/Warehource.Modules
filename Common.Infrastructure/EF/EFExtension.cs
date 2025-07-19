using System.Linq.Expressions;
using Common.Domain.EF;
using Microsoft.EntityFrameworkCore;

namespace Common.Infrastructure.EF;

public static class EFExtension
{
    /// <summary>
    ///     软删除
    /// </summary>
    /// <param name="modelBuilder"></param>
    public static void EnableSoftDeletionGlobalFilter(this ModelBuilder modelBuilder)
    {
        var entityTypesHasSoftDeletion = modelBuilder.Model.GetEntityTypes()
            .Where(e => e.ClrType.IsAssignableTo(typeof(ISoftDelete)));

        foreach (var entityType in entityTypesHasSoftDeletion)
        {
            var isDeletedProperty = entityType.FindProperty(nameof(ISoftDelete.IsDeleted));
            var parameter = Expression.Parameter(entityType.ClrType, "p");
            var filter =
                Expression.Lambda(Expression.Not(Expression.Property(parameter, isDeletedProperty.PropertyInfo)),
                    parameter);
            entityType.SetQueryFilter(filter);
        }
    }

    public static IQueryable<T> Query<T>(this DbContext ctx) where T : class
    {
        return ctx.Set<T>().AsNoTracking();
    }
}