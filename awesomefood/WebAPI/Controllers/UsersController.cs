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
        private static IUserInteractor _userInteractor;

        public UsersController(IUserInteractor userInteractor)
        {
            _userInteractor = userInteractor;
        }

        // GET api/users/5f965c3c-6f8c-4729-b969-cc79c77f90b8
        [HttpGet("{id}", Name = "GetUser")]
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
            var userId = _userInteractor.CreateUser(Models.User.MapToEntity(user));
            user.Id = userId;
            return CreatedAtRoute("GetUser", new { id = userId }, user);
        }

        // PUT api/users/5f965c3c-6f8c-4729-b969-cc79c77f90b8
        [HttpPut("{id}")]
        public IActionResult Put(Guid id, [FromBody]Models.User user)
        {
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

    }
}
