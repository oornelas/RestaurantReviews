using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AwesomeFood.WebAPI.Models;
using AwesomeFood.Common.Exceptions;
using AwesomeFood.Interactors;
using AwesomeFood.DataAccess;
using AwesomeFood.Contracts.Entities;
using AwesomeFood.Contracts.Interactors;

namespace AwesomeFood.WebAPI.Controllers
{
    [Route("api/[controller]")]
    public class RestaurantsController : Controller
    {
        private static IRestaurantInteractor _restaurantInteractor;

        public RestaurantsController(IRestaurantInteractor restaurantInteractor)
        {
            _restaurantInteractor = restaurantInteractor;
        }

        // GET api/restaurants/5f965c3c-6f8c-4729-b969-cc79c77f90b8
        [HttpGet("{id}", Name = "GetRestaurant")]
        public IActionResult Get(Guid id)
        {
            try 
            {
                return new ObjectResult(Models.Restaurant.MapFromEntity(_restaurantInteractor.GetRestaurant(id)));
            }
            catch(EntityNotFoundException)
            {
                return new NotFoundResult();
            }
        }

        // GET api/restaurants/
        [HttpGet]
        public IActionResult Get()
        {
            try 
            {
                //TODO: Put max results in config
                return new ObjectResult(_restaurantInteractor.ListRestaurants(100).Select(r => Models.Restaurant.MapFromEntity(r)));
            }
            catch(EntityNotFoundException)
            {
                return new NotFoundResult();
            }
        }

        // GET api/restaurants/{state}/{city}
        [HttpGet("{state}/{city}")]
        public IActionResult Get(string state, string city)
        {
            try 
            {
                //TODO: Put max results in config
                return new ObjectResult(_restaurantInteractor.ListRestaurantsByCity(city,state,100).Select(r => Models.Restaurant.MapFromEntity(r)));
            }
            catch(EntityNotFoundException)
            {
                return new NotFoundResult();
            }
        }

        // POST api/restaurants
        [HttpPost]
        public IActionResult Post([FromBody]Models.Restaurant restaurant)
        {
            var restaurantId = _restaurantInteractor.CreateRestaurant(Models.Restaurant.MapToEntity(restaurant));
            restaurant.Id = restaurantId;
            restaurant.AwesomenessLevel = 0;
            return CreatedAtRoute("GetRestaurant", new { id = restaurantId }, restaurant);
        }

        // PUT api/restaurants/5f965c3c-6f8c-4729-b969-cc79c77f90b8
        [HttpPut("{id}")]
        public IActionResult Put(Guid id, [FromBody]Models.Restaurant restaurant)
        {
            try 
            {
                restaurant.Id = id;
                _restaurantInteractor.UpdateRestaurant(Models.Restaurant.MapToEntity(restaurant));
                return new OkResult();
            }
            catch(EntityNotFoundException)
            {
                return new NotFoundResult();
            }
        }
    }
}