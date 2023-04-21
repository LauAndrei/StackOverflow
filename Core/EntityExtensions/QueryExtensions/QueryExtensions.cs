using System.Linq.Expressions;

namespace Core.EntityExtensions.QueryExtensions;

public static class QueryExtensions
{
    public static IQueryable<T> WhereIf<T>(this IQueryable<T> query, Expression<Func<T, bool>> predicate, bool condition)
    {
        return condition ? query.Where(predicate) : query;
    }
}