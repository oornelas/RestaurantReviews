using System;
using System.Collections.Generic;
using AwesomeFood.Contracts.Entities;

namespace AwesomeFood.Contracts.DataAccess
{
    public interface IRestaurantDataAccess
    {
        void SaveRestaurant(IRestaurant restaurant);
        IRestaurant GetRestaurant(Guid restaurantId);
        IEnumerable<IRestaurant> ListRestaurants(int maximumResults);
        IEnumerable<IRestaurant> ListRestaurantsByCity(string city, string state, int maximumResults);
    }
}