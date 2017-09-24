using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AwesomeFood.WebAPI.Models;

namespace AwesomeFood.WebAPI.Controllers
{
    [Route("api/[controller]")]
    public class UsersController : Controller
    {

        // GET api/users/5f965c3c-6f8c-4729-b969-cc79c77f90b8
        [HttpGet("{id}")]
        public User Get(Guid id)
        {
            return new User();
        }

        // POST api/users
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/users/5f965c3c-6f8c-4729-b969-cc79c77f90b8
        [HttpPut("{id}")]
        public void Put(Guid id, [FromBody]string value)
        {
        }

    }
}
