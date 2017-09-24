using System;

namespace AwesomeFood.Contracts.Entities
{
    public interface IUser : IEntity
    {
        string Email { get; set; }
        string DisplayName { get; set; }
        string FirstName { get; set; }
        string LastName { get; set; }
    }
}
