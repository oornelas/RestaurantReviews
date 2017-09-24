using System;
using AwesomeFood.Contracts.Repositories;

namespace AwesomeFood.Repositories
{
    public class PaginationParameters : IPaginationParameters
    {
        public int MaximumRecords { get; set; }
        public int Offset { get; set; }
    }
}