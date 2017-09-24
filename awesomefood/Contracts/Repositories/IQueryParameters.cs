using System;
using AwesomeFood.Common;

namespace AwesomeFood.Contracts.Repositories
{
    public interface IQueryParameters<T>
    {
        Func<T,bool> Filter  { get; set; }
        IOrderByParameters<T> OrderByParameters { get; set; }
        IPaginationParameters Pagination  { get; set; }
    }
}