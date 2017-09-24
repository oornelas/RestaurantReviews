using System;
using AwesomeFood.Contracts.Entities;

namespace AwesomeFood.Contracts.Interactors
{
    public interface IUserInteractor
    {
        Guid CreateUser(IUser user);
        void UpdateUser(IUser user);
        IUser GetUser(Guid userId);
    }
}