using JobBoard.Application.Common.Objects;
using JobBoard.Domain;
using System.Linq.Expressions;

namespace JobBoard.Application.Common.Extensions
{
    public static class Extensions
    {
        public static IQueryable<T> OrderBy<T, U>(this IQueryable<T> collection, Expression<Func<T, U>> expression, bool asc)
        {
            if(asc)
                return collection.OrderBy(expression);
            return collection.OrderByDescending(expression);
        }
    }
}
