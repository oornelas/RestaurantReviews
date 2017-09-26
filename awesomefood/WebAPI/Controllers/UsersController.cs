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
    public class UsersController : Controller
    {
        private static IRestaurantInteractor _restaurantInteractor;
        private static IDishInteractor _dishInteractor;
        private static IUserInteractor _userInteractor;
        private static IDishReviewInteractor _dishReviewInteractor;

        public UsersController(IRestaurantInteractor restaurantInteractor, IDishInteractor dishInteractor, IUserInteractor userInteractor, IDishReviewInteractor dishReviewInteractor)
        {
            _restaurantInteractor = restaurantInteractor;
            _dishInteractor = dishInteractor;
            _userInteractor = userInteractor;
            _dishReviewInteractor = dishReviewInteractor;
        }

        // GET api/users/5f965c3c-6f8c-4729-b969-cc79c77f90b8
        [HttpGet("{id:guid}", Name = "GetUser")]
        public IActionResult Get(Guid id)
        {
            try 
            {
                return new ObjectResult(Models.User.MapFromEntity(_userInteractor.GetUser(id)));
            }
            catch(EntityNotFoundException)
            {
                return new NotFoundResult();
            }
        }

        // POST api/users
        [HttpPost]
        public IActionResult Post([FromBody]Models.User user)
        {
            if (!ModelState.IsValid)
            {
                return new BadRequestResult();
            }

            var userId = _userInteractor.CreateUser(Models.User.MapToEntity(user));
            user.Id = userId;
            return CreatedAtRoute("GetUser", new { id = userId }, user);
        }

        // PUT api/users/5f965c3c-6f8c-4729-b969-cc79c77f90b8
        [HttpPut("{id:guid}")]
        public IActionResult Put(Guid id, [FromBody]Models.User user)
        {
            if (!ModelState.IsValid)
            {
                return new BadRequestResult();
            }

            try 
            {
                user.Id = id;
                _userInteractor.UpdateUser(Models.User.MapToEntity(user));
                return new OkResult();
            }
            catch(EntityNotFoundException)
            {
                return new NotFoundResult();
            }
        }

        // GET api/users/5f965c3c-6f8c-4729-b969-cc79c77f90b8/reviews/5f965c3c-6f8c-4729-b969-cc79c77f90b8
        [HttpGet("{userId:guid}/reviews/{dishReviewId:guid}")]
        public IActionResult GetDishReview(Guid userId, Guid dishReviewId)
        {
            try 
            {
                var review = Models.DishReview.MapFromEntity(_dishReviewInteractor.GetDishReview(dishReviewId));
                review.Dish = Models.Dish.MapFromEntity(_dishInteractor.GetDish(review.DishId));
                review.Restaurant = Models.Restaurant.MapFromEntity(_restaurantInteractor.GetRestaurant(review.Dish.RestaurantId));
                return new ObjectResult(review);
            }
            catch(EntityNotFoundException)
            {
                return new NotFoundResult();
            }
        }

        // GET api/users/5f965c3c-6f8c-4729-b969-cc79c77f90b8/reviews
        [HttpGet("{userId:guid}/reviews")]
        public IActionResult GetDishReviews(Guid userId)
        {
            try 
            {
                //TODO: Put max results in config
                return new ObjectResult(_dishReviewInteractor.ListDishReviewsByUser(userId,100).Select(r => Models.DishReview.MapFromEntity(r)));
            }
            catch(EntityNotFoundException)
            {
                return new NotFoundResult();
            }
        }

        // DELETE api/users/5f965c3c-6f8c-4729-b969-cc79c77f90b8/reviews/5f965c3c-6f8c-4729-b969-cc79c77f90b8
        [HttpDelete("{userId:guid}/reviews/{dishReviewId:guid}")]
        public IActionResult DeleteDishReview(Guid userId, Guid dishReviewId)
        {
            try 
            {
                _dishReviewInteractor.DeleteDishReview(dishReviewId);
                return new OkResult();
            }
            catch(EntityNotFoundException)
            {
                return new NotFoundResult();
            }
        }

    }
}
