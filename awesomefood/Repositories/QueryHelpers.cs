using System;
using System.Collections.Generic;
using System.Linq;
using AwesomeFood.Common;
using AwesomeFood.Contracts.Repositories;

namespace AwesomeFood.Repositories
{
    public static class QueryHelpers
    {
        public static IEnumerable<T> AddFiltersToQuery<T>(this IEnumerable<T> query, Func<T,bool> filter)
        {
            if (filter != null)
            {
                query = query.Where(e => filter(e));
            }

            return query;
        }

        public static IEnumerable<T> AddPaginationToQuery<T>(this IEnumerable<T> query, IPaginationParameters pagination)
        {
            if (pagination != null)
            {
                query = query.Skip(pagination.Offset).Take(pagination.MaximumRecords);
            }

            return query;
        }

        public static IEnumerable<T> AddSortingToQuery<T>(this IEnumerable<T> query, IOrderByParameters<T> orderByParameters)
        {
            if (orderByParameters?.OrderBy != null)
            {
                if (orderByParameters.OrderByDirection == OrderByDirection.Ascending)
                {
                    query = query.OrderBy(x => orderByParameters.OrderBy(x));
                }
                else
                {
                    query = query.OrderByDescending(x => orderByParameters.OrderBy(x));
                }
            }

            return query;
        }
    }
}