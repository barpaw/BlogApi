using System.Linq.Expressions;
using BlogApi.Shared.Helpers.Queryable;
using Microsoft.EntityFrameworkCore;

namespace BlogApi.Shared.Extensions.Queryable;

public static class QueryableExtensions
{
    public static IQueryable<T> ApplySorting<T>(this IQueryable<T> query, string orderBy)
    {
        return query;
    }

    public static IQueryable<T> ApplyFiltering<T>(this IQueryable<T> query, IEnumerable<FilterCriteria> filters)
    {
        if (filters == null || !filters.Any())
        {
            return query;
        }

        var parameterExpression = Expression.Parameter(typeof(T), "x");
        Expression combinedExpression = null;

        foreach (var filter in filters)
        {
            var propertyName = filter.FieldName;
            var property = Expression.Property(parameterExpression, propertyName);
            var value = Expression.Constant(filter.Value, filter.Value.GetType());

            Expression binaryExpression = filter.Operation switch
            {
                "eq" => Expression.Equal(property, value),
                "gt" => Expression.GreaterThan(property, value),
                "lt" => Expression.LessThan(property, value),


                _ => throw new ArgumentException($"Nieobsługiwana operacja: {filter.Operation}")
            };

            combinedExpression = combinedExpression == null
                ? binaryExpression
                : Expression.AndAlso(combinedExpression, binaryExpression);
        }

        var lambda = Expression.Lambda<Func<T, bool>>(combinedExpression, parameterExpression);
        return query.Where(lambda);
    }


    public static async Task<PagedResult<T>> GetPagedAsync<T>(this IQueryable<T> query, QueryParameters queryParams)
        where T : class
    {
        if (queryParams.Page < 1)
        {
            throw new ArgumentException("Numer strony powinien być większy lub równy 1.", nameof(queryParams.Page));
        }

        if (queryParams.PageSize < 1)
        {
            throw new ArgumentException("Rozmiar strony powinien być większy niż 0.", nameof(queryParams.PageSize));
        }

        const int MaxPageSize = 100;
        if (queryParams.PageSize > MaxPageSize)
        {
            throw new ArgumentException($"Rozmiar strony nie może przekraczać {MaxPageSize}.", nameof(queryParams.PageSize));
        }

        var result = new PagedResult<T>
        {
            CurrentPage = queryParams.Page,
            PageSize = queryParams.PageSize,
            TotalItems = await query.CountAsync()
        };

        var pageCount = (double)result.TotalItems / queryParams.PageSize;
        result.TotalPages = (int)Math.Ceiling(pageCount);

        var skip = (queryParams.Page - 1) * queryParams.PageSize;
        result.Items = await query.Skip(skip).Take(queryParams.PageSize).ToListAsync();

        return result;
    }
}