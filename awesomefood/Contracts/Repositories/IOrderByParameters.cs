using System;
using AwesomeFood.Common;

namespace AwesomeFood.Contracts.Repositories
{
    public interface IOrderByParameters<T>
    {
        Func<T,IComparable> OrderBy {get; set;}
        OrderByDirection OrderByDirection  {get; set;}
    }
}