using System;

namespace AwesomeFood.Contracts.Entities
{
    public interface IDish : IUserCreatedEntity
    {
        Guid RestaurantId { get; set; }
        string Name { get; set; }
        string Description { get; set; }
        byte AwesomenessLevel { get; set; }
    }
}