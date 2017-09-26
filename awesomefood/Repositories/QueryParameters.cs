using System;
using System.Linq.Expressions;
using AwesomeFood.Contracts.Repositories;

namespace AwesomeFood.Repositories
{
    public class QueryParameters<T> : IQueryParameters<T>
    {
        public Expression<Func<T, bool>> Filter { get; set; }
        public IOrderByParameters<T> OrderByParameters { get; set; }
        public IPaginationParameters Pagination { get; set; }
    }
}