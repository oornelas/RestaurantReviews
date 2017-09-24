using System;
using AwesomeFood.Common;

namespace AwesomeFood.Contracts.Entities
{
    public interface IUserCredentials : IEntity
    {
        Guid UserId { get; set; }
        AuthenticationType AuthenticationType { get; set; }
        string AuthenticationToken { get; set; }
    }
}
