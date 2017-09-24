using System;
using System.Collections.Generic;
using AwesomeFood.Contracts.Entities;

namespace AwesomeFood.Contracts.DataAccess
{
    public interface IDishDataAccess
    {
        void SaveDish(IDish dish);
        IDish GetDish(Guid dishId);
        IEnumerable<IDish> ListRestaurantDishes(Guid restaurantId);
    }
}