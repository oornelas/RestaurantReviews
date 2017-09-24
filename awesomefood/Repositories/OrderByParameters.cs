using System;
using AwesomeFood.Common;
using AwesomeFood.Contracts.Repositories;

namespace AwesomeFood.Repositories
{
    public class OrderByParameters<T> : IOrderByParameters<T>
    {
        public Func<T, IComparable> OrderBy { get; set; }
        public OrderByDirection OrderByDirection { get; set; }
    }
}
