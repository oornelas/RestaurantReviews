using System;
using System.Collections.Generic;
using AwesomeFood.Common.Exceptions;
using AwesomeFood.Contracts.DataAccess;
using AwesomeFood.Contracts.Entities;
using AwesomeFood.Contracts.Interactors;

namespace AwesomeFood.Interactors
{
    public class DishInteractor : IDishInteractor
    {
        private readonly IDishDataAccess _dishDataAccess;

        public DishInteractor(IDishDataAccess dishDataAccess)
        {
            _dishDataAccess = dishDataAccess;
        }
        public Guid CreateDish(IDish dish)
        {
            if (dish == null)
            {
                throw new ArgumentNullException(nameof(dish));
            }

            dish.id = Guid.NewGuid();
            dish.CreatedOn = DateTime.UtcNow;
            dish.ModifiedOn = DateTime.UtcNow;
            
            _dishDataAccess.SaveDish(dish);

            return dish.id;
        }

        public IDish GetDish(Guid dishId)
        {
            var dish = _dishDataAccess.GetDish(dishId);

            if (dish == null)
            {
                throw new EntityNotFoundException(dishId);
            }

            return dish;
        }

        public IEnumerable<IDish> ListRestaurantDishes(Guid restaurantId)
        {
            return _dishDataAccess.ListRestaurantDishes(restaurantId);
        }

        public void UpdateDish(IDish dish)
        {
            if (dish == null)
            {
                throw new ArgumentNullException(nameof(dish));
            }

            var existingDish = GetDish(dish.id);

            UpdateDishFields(existingDish, dish);

            _dishDataAccess.SaveDish(existingDish);
        }

        private static void UpdateDishFields(IDish existingDish, IDish dish)
        {
           existingDish.Description = dish.Description;
           existingDish.ModifiedOn = DateTime.UtcNow;
           existingDish.Name = dish.Name;
        }
    }
}