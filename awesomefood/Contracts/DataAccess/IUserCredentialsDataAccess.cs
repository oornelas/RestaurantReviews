using System;
using AwesomeFood.Common;
using AwesomeFood.Contracts.Entities;

namespace AwesomeFood.Contracts.DataAccess
{
    public interface IUserCredentialsDataAccess
    {
        void SaveUserCredentials(IUserCredentials userCredentials);
        IUserCredentials GetUserCredentials(Guid userId, AuthenticationType authenticationType);
    }
}