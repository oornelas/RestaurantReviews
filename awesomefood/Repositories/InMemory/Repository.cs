using System;
using System.Collections.Generic;
using System.Linq;
using AwesomeFood.Common;
using AwesomeFood.Contracts.Entities;
using AwesomeFood.Contracts.Repositories;
using AwesomeFood.Repositories;

namespace Repositories.InMemory
{
    public class Repository<T> : IRepository<T> where T: IEntity
    {
        protected readonly Dictionary<Guid, T> _entityDictionary;

        public Repository()
        {
            _entityDictionary = new Dictionary<Guid,T>();
        }

        public void Delete(Guid id)
        {
            if (_entityDictionary.ContainsKey(id))
            {
                _entityDictionary.Remove(id);
            }
        }

        public T Get(Guid id)
        {
            return _entityDictionary.ContainsKey(id) ? _entityDictionary[id] : default(T);
        }

        public IEnumerable<T> Query(IQueryParameters<T> parameters)
        {
            var query = _entityDictionary.Values.AsEnumerable();

            if (parameters == null) return query;

            return query.AddFiltersToQuery(parameters.Filter)
                        .AddSortingToQuery(parameters.OrderByParameters)
                        .AddPaginationToQuery(parameters.Pagination)
                        .ToList();
        }

        public void Save(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            if (_entityDictionary.ContainsKey(entity.Id))
            {
                _entityDictionary[entity.Id] = entity;
            }
            else
            {
                _entityDictionary.Add(entity.Id, entity);
            }
        }
    }
}