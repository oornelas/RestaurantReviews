using System;
using System.Collections.Generic;
using System.Linq;
using AwesomeFood.Common;
using AwesomeFood.Contracts.DataAccess;
using AwesomeFood.Contracts.Entities;
using AwesomeFood.Contracts.Repositories;
using AwesomeFood.Repositories;

namespace AwesomeFood.DataAccess
{
    public class UserCredentialsDataAccess : IUserCredentialsDataAccess
    {
        private readonly IRepository<IUserCredentials> _credentialsRepository;

        public UserCredentialsDataAccess(IRepository<IUserCredentials> credentialsRepository)
        {
            _credentialsRepository = credentialsRepository;
        }
        public IUserCredentials GetUserCredentials(Guid userId, AuthenticationType authenticationType)
        {
            var queryParams = new QueryParameters<IUserCredentials>()
            {
                Filter = (user) => user.Id == userId && user.AuthenticationType == authenticationType
            };

            return _credentialsRepository.Query(queryParams).First();
        }

        public void SaveUserCredentials(IUserCredentials userCredentials)
        {
            if (userCredentials == null)
            {
                throw new ArgumentNullException(nameof(userCredentials));
            }

            _credentialsRepository.Save(userCredentials);
        }
    }
}