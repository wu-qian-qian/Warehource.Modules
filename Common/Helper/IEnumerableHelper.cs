using System.Linq.Expressions;

namespace Common.Helper;

public static class IEnumerableHelper
{
    public static IEnumerable<T> WhereIf<T>(this IEnumerable<T> list, bool condition, Func<T, bool> predicate)
    {
        return condition ? list.Where(predicate) : list;
    }

    public static IEnumerable<T> WhereIf<T>(this IEnumerable<T> list, Func<T, bool> condition, Func<T, bool> predicate)
    {
        return list.All(condition) ? list.Where(predicate) : list;
    }

    public static IQueryable<T> WhereIf<T>(this IQueryable<T> list, bool condition
        , Expression<Func<T, bool>> predicate)
    {
        return condition ? list.Where(predicate) : list;
    }

    public static IQueryable<T> ToPageBySortAsc<T>(this IQueryable<T> list, int skinCount, int total
        , Expression<Func<T, DateTime>> predicate)
    {
        list.OrderByDescending(predicate);
        return list.Skip(skinCount).Take(total);
    }
}