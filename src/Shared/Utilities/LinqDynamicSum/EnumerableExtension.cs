using System.Linq.Expressions;

namespace Utilities.LinqDynamicSum;

public static class EnumerableExtension
{
    public static TSource DynamicSum<TSource>(this IEnumerable<TSource> source)
    {
        if (!source.Any())
        {
            return Activator.CreateInstance<TSource>();
        }

        var addFunc = GenAddFunc<TSource>();
        return source.Aggregate(addFunc);

    }

    public static TSource DynamicSum<TSource>(this IEnumerable<TSource> source, TSource @default)
    {
        if (!source.Any())
        {
            return @default;
        }

        var addFunc = GenAddFunc<TSource>();
        return source.Aggregate(addFunc);

    }

    private static Func<T, T, T> GenAddFunc<T>()
    {
        var parameterExpression1 = Expression.Parameter(typeof(T));
        var parameterExpression2 = Expression.Parameter(typeof(T));
        var addExpression = Expression.Add(
            Expression.Convert(parameterExpression1, typeof(T)),
            Expression.Convert(parameterExpression2, typeof(T)));
        return Expression.Lambda<Func<T, T, T>>(
                Expression.Convert(addExpression, typeof(T)),
                parameterExpression1,
                parameterExpression2)
            .Compile();
    }

    public static SummaryEntity<TSource> CalculateSummaryEntity<TSource>(this IEnumerable<TSource> source)
    {
        return new SummaryEntity<TSource>
        {
            Entities = source.ToList(),
            Total = source.DynamicSum()
        };
    }
}