using System;
using System.Collections.Generic;
using AwesomeFood.Common;
using AwesomeFood.Contracts.DataAccess;
using AwesomeFood.Contracts.Entities;
using AwesomeFood.Contracts.Repositories;
using AwesomeFood.Repositories;

namespace AwesomeFood.DataAccess
{
    public class RestaurantDataAccess : IRestaurantDataAccess
    {
        private readonly IRepository<IRestaurant> _restaurantRepository;

        public RestaurantDataAccess(IRepository<IRestaurant> restaurantRepository)
        {
            _restaurantRepository = restaurantRepository ?? throw new ArgumentNullException(nameof(restaurantRepository));
        }
        public IRestaurant GetRestaurant(Guid restaurantId)
        {
            return _restaurantRepository.Get(restaurantId);
        }

        public IEnumerable<IRestaurant> ListRestaurants(int maximumResults)
        {
            var queryParams = new QueryParameters<IRestaurant>()
            {
                OrderByParameters = new OrderByParameters<IRestaurant>() { OrderBy = (restaurant) => restaurant.AwesomenessLevel, OrderByDirection = OrderByDirection.Descending },
                Pagination = new PaginationParameters() { MaximumRecords = maximumResults, Offset = 0 }
            };

            return _restaurantRepository.Query(queryParams);
        }

        public IEnumerable<IRestaurant> ListRestaurantsByCity(string city, string state, int maximumResults)
        {
            var queryParams = new QueryParameters<IRestaurant>()
            {
                Filter = (restaurant) => restaurant.City.ToLower() == city.ToLower()  && restaurant.State.ToLowerInvariant() == state.ToLower(),
                OrderByParameters = new OrderByParameters<IRestaurant>() { OrderBy = (restaurant) => restaurant.AwesomenessLevel, OrderByDirection = OrderByDirection.Descending },
                Pagination = new PaginationParameters() { MaximumRecords = maximumResults, Offset = 0 }
            };

            return _restaurantRepository.Query(queryParams);
        }

        public void SaveRestaurant(IRestaurant restaurant)
        {
            _restaurantRepository.Save(restaurant);
        }
    }
}