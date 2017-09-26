using System;
using System.Collections.Generic;
using AwesomeFood.Common.Exceptions;
using AwesomeFood.Contracts.DataAccess;
using AwesomeFood.Contracts.Entities;
using AwesomeFood.Contracts.Interactors;

namespace AwesomeFood.Interactors
{
    public class RestaurantInteractor : IRestaurantInteractor
    {
        private readonly IRestaurantDataAccess _restaurantDataAccess;

        public RestaurantInteractor(IRestaurantDataAccess restaurantDataAccess)
        {
            _restaurantDataAccess = restaurantDataAccess;
        }
        public Guid CreateRestaurant(IRestaurant restaurant)
        {
            if (restaurant == null)
            {
                throw new ArgumentNullException(nameof(restaurant));
            }

            restaurant.id = Guid.NewGuid();
            restaurant.CreatedOn = DateTime.UtcNow;
            restaurant.ModifiedOn = DateTime.UtcNow;
            
            _restaurantDataAccess.SaveRestaurant(restaurant);

            return restaurant.id;
        }

        public IRestaurant GetRestaurant(Guid restaurantId)
        {
            var restaurant = _restaurantDataAccess.GetRestaurant(restaurantId);

            if (restaurant == null)
            {
                throw new EntityNotFoundException(restaurantId);
            }

            return restaurant;
        }

        public IEnumerable<IRestaurant> ListRestaurants(int maximumResults)
        {
            return _restaurantDataAccess.ListRestaurants(maximumResults);
        }

        public IEnumerable<IRestaurant> ListRestaurantsByCity(string city, string state, int maximumResults)
        {
            if (string.IsNullOrWhiteSpace(city))
            {
                throw new ArgumentException("City parameter can't be empty.", nameof(city));
            }

            if (string.IsNullOrWhiteSpace(state))
            {
                throw new ArgumentException("State parameter can't be empty.", nameof(state));
            }

            return _restaurantDataAccess.ListRestaurantsByCity(city,state,maximumResults);
        }

        public void UpdateRestaurant(IRestaurant restaurant)
        {
            if (restaurant == null)
            {
                throw new ArgumentNullException(nameof(restaurant));
            }

            var existingRestaurant = GetRestaurant(restaurant.id);

            UpdateRestaurantFields(existingRestaurant, restaurant);

            _restaurantDataAccess.SaveRestaurant(existingRestaurant);
        }

        private static void UpdateRestaurantFields(IRestaurant existingRestaurant, IRestaurant restaurant)
        {
           existingRestaurant.AddressLine1 = restaurant.AddressLine1;
           existingRestaurant.AddressLine2 = restaurant.AddressLine2;
           existingRestaurant.City = restaurant.City;
           existingRestaurant.Email = restaurant.Email;
           existingRestaurant.Hours = restaurant.Hours;
           existingRestaurant.ModifiedByUserId = restaurant.ModifiedByUserId;
           existingRestaurant.ModifiedOn = DateTime.UtcNow;
           existingRestaurant.Name = restaurant.Name;
           existingRestaurant.Phone = restaurant.Phone;
           existingRestaurant.State = restaurant.State;
           existingRestaurant.TimeZone = restaurant.TimeZone;
           existingRestaurant.Website = restaurant.Website;
           existingRestaurant.ZipCode = restaurant.ZipCode;
        }
    }
}