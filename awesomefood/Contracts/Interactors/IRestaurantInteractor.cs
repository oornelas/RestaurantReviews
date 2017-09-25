using System;
using System.Collections.Generic;
using AwesomeFood.Contracts.Entities;

namespace AwesomeFood.Contracts.Interactors
{
    public interface IRestaurantInteractor
    {
        Guid CreateRestaurant(IRestaurant restaurant);
        void UpdateRestaurant(IRestaurant restaurant);
        IRestaurant GetRestaurant(Guid restaurantId);
        IEnumerable<IRestaurant> ListRestaurants(int maximumResults);
        IEnumerable<IRestaurant> ListRestaurantsByCity(string city, string state, int maximumResults);

    }
}