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
        private static IDishInteractor _dishInteractor;

        public RestaurantsController(IRestaurantInteractor restaurantInteractor, IDishInteractor dishInteractor)
        {
            _restaurantInteractor = restaurantInteractor;
            _dishInteractor = dishInteractor;
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
                return new ObjectResult(_restaurantInteractor.ListRestaurantsByCity(city.Replace("-"," "),state.Replace("-"," "),100).Select(r => Models.Restaurant.MapFromEntity(r)));
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
            restaurant.AwesomenessLevel = 0;
            var restaurantId = _restaurantInteractor.CreateRestaurant(Models.Restaurant.MapToEntity(restaurant));
            restaurant.Id = restaurantId;            
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

        // GET api/restaurants/5f965c3c-6f8c-4729-b969-cc79c77f90b8/dishes
        [HttpGet("{restaurantId}/dishes")]
        public IActionResult GetDishes(Guid restaurantId)
        {
            try 
            {
                //TODO: Put max results in config
                return new ObjectResult(_dishInteractor.ListRestaurantDishes(restaurantId).Select(r => Models.Dish.MapFromEntity(r)));
            }
            catch(EntityNotFoundException)
            {
                return new NotFoundResult();
            }
        }

        // GET api/restaurants/5f965c3c-6f8c-4729-b969-cc79c77f90b8/dishes/7f467c8c-Bf4c-4329-b9A9-cc00c77f90b8
        [HttpGet("{restaurantId}/dishes/{dishId}", Name = "GetDish")]
        public IActionResult GetDish(Guid restaurantId, Guid dishId)
        {
            try 
            {
                return new ObjectResult(Models.Dish.MapFromEntity(_dishInteractor.GetDish(dishId)));
            }
            catch(EntityNotFoundException)
            {
                return new NotFoundResult();
            }
        }

        // PUT api/restaurants/5f965c3c-6f8c-4729-b969-cc79c77f90b8/dishes/7f467c8c-Bf4c-4329-b9A9-cc00c77f90b8
        [HttpPut("{restaurantId}/dishes/{dishId}")]
        public IActionResult PutDish(Guid restaurantId, Guid dishId, [FromBody]Models.Dish dish)
        {
            try 
            {
                dish.Id = dishId;
                dish.RestaurantId = restaurantId;
                _dishInteractor.UpdateDish(Models.Dish.MapToEntity(dish));
                return new OkResult();
            }
            catch(EntityNotFoundException)
            {
                return new NotFoundResult();
            }
        }

        // POST api/restaurants/5f965c3c-6f8c-4729-b969-cc79c77f90b8/dishes
        [HttpPost("{restaurantId}/dishes")]
        public IActionResult PostDish(Guid restaurantId, [FromBody]Models.Dish dish)
        {
            dish.RestaurantId = restaurantId;
            dish.AwesomenessLevel = 0;
            var dishId = _dishInteractor.CreateDish(Models.Dish.MapToEntity(dish));
            dish.Id = dishId;
            return CreatedAtRoute("GetDish", new { restaurantId, dishId }, dish);
        }
    }
}