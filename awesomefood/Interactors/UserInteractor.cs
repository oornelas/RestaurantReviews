using System;
using AwesomeFood.Common.Exceptions;
using AwesomeFood.Contracts.DataAccess;
using AwesomeFood.Contracts.Entities;
using AwesomeFood.Contracts.Interactors;

namespace AwesomeFood.Interactors
{
    public class UserInteractor : IUserInteractor
    {
        private readonly IUserDataAccess _userDataAccess;

        public UserInteractor(IUserDataAccess userDataAccess)
        {
            if (userDataAccess == null)
            {
                throw new ArgumentNullException(nameof(userDataAccess));
            }

            _userDataAccess = userDataAccess;
        }

        public Guid CreateUser(IUser user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            user.Id = Guid.NewGuid();
            user.CreatedOn = DateTime.UtcNow;
            user.ModifiedOn = DateTime.UtcNow;
            
            _userDataAccess.SaveUser(user);

            return user.Id;
        }

        public IUser GetUser(Guid userId)
        {
            var user = _userDataAccess.GetUser(userId);

            if (user == null)
            {
                throw new UserNotFoundException(userId);
            }

            return user;
        }

        public void UpdateUser(IUser user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            var existingUser = GetUser(user.Id);

            UpdateUserFields(existingUser, user);

            _userDataAccess.SaveUser(existingUser);
        }

        private static void UpdateUserFields(IUser existingUser, IUser user)
        {
           existingUser.DisplayName = user.DisplayName;
           existingUser.Email = user.Email;
           existingUser.FirstName = user.FirstName;
           existingUser.LastName = user.LastName;
           existingUser.ModifiedOn = DateTime.UtcNow;
        }
    }
}
