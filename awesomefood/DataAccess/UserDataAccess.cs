using System;
using System.Collections.Generic;
using System.Linq;
using AwesomeFood.Contracts.DataAccess;
using AwesomeFood.Contracts.Entities;
using AwesomeFood.Contracts.Repositories;
using AwesomeFood.Repositories;

namespace AwesomeFood.DataAccess
{
    public class UserDataAccess : IUserDataAccess
    {
        private readonly IRepository<IUser> _userRepository;

        public UserDataAccess(IRepository<IUser> userRepository)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        }
        public IUser FindUserByEmail(string email)
        {
            var queryParams = new QueryParameters<IUser>()
            {
                Filter = (user) => user.Email.ToLower() == email.ToLower()
            };

            return _userRepository.Query(queryParams).First();
        }

        public IUser GetUser(Guid userId)
        {
            return _userRepository.Get(userId);
        }

        public void SaveUser(IUser user)
        {
            _userRepository.Save(user);
        }
    }
}