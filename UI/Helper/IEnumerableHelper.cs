namespace UI.Helper;

public static class IEnumerableHelper
{
    public static IEnumerable<T> WhereIf<T>(this IEnumerable<T> list, bool condition, Func<T, bool> predicate)
    {
        return condition ? list.Where(predicate) : list;
    }
}