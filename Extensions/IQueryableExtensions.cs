using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using VehicleDealer.Persistence.DatabaseModel;

namespace VehicleDealer.Extensions
{
    public static class IQueryableExtensions
    {
       public static IQueryable<T> ApplyOrdering<T>(this IQueryable<T> query, IQueryObject queryObj, Dictionary<string, Expression<Func<T, object>>> columnsMap)
        {
            if (queryObj.IsSortAscending)
                query = query.OrderBy(columnsMap[queryObj.SortBy]);
            else
                query = query.OrderByDescending(columnsMap[queryObj.SortBy]);
            return query;
        }
    }
}