using System;
using AwesomeFood.Contracts.Entities;

namespace AwesomeFood.Contracts.DataAccess
{
    public interface IUserDataAccess
    {
        void SaveUser(IUser user);
        IUser GetUser(Guid userId);
        IUser FindUserByEmail(string email);
    }
}