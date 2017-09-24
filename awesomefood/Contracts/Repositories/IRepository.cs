using System;
using System.Collections.Generic;
using AwesomeFood.Common;
using AwesomeFood.Contracts.Entities;

namespace AwesomeFood.Contracts.Repositories
{
    public interface IRepository<T> where T : IEntity
    {
        T Get(Guid id);
        IEnumerable<T> Query(IQueryParameters<T> parameters);
        void Save(T entity);
        void Delete(Guid id);
    }
    
}