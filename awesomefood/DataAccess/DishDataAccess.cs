using System;
using System.Collections.Generic;
using AwesomeFood.Common;
using AwesomeFood.Contracts.DataAccess;
using AwesomeFood.Contracts.Entities;
using AwesomeFood.Contracts.Repositories;
using AwesomeFood.Repositories;

namespace AwesomeFood.DataAccess
{
    public class DishDataAccess : IDishDataAccess
    {
        private readonly IRepository<IDish> _dishRepository;

        public DishDataAccess(IRepository<IDish> dishRepository)
        {
            _dishRepository = dishRepository ?? throw new ArgumentNullException(nameof(dishRepository));
        }

        public IDish GetDish(Guid dishId)
        {
            return _dishRepository.Get(dishId);
        }

        public IEnumerable<IDish> ListRestaurantDishes(Guid restaurantId)
        {
            var queryParams = new QueryParameters<IDish>()
            {
                Filter = (dish) => dish.RestaurantId == restaurantId,
                OrderByParameters = new OrderByParameters<IDish>() { OrderBy = (dish) => dish.AwesomenessLevel, OrderByDirection = OrderByDirection.Descending }
            };

            return _dishRepository.Query(queryParams);
        }

        public void SaveDish(IDish dish)
        {
            _dishRepository.Save(dish);
        }
    }
}