using System;
using System.Collections.Generic;
using AwesomeFood.Contracts.Entities;

namespace AwesomeFood.Contracts.Interactors
{
    public interface IDishInteractor
    {
        Guid CreateDish(IDish dish);
        void UpdateDish(IDish dish);
        IDish GetDish(Guid dishId);
        IEnumerable<IDish> ListRestaurantDishes(Guid restaurantId);
    }
}