using System;

namespace AwesomeFood.Contracts.Entities
{
    public interface IDishReview :  IUserCreatedEntity
    {
        Guid DishId { get; set; }
        byte AwesomenessLevel { get; set; }
        string Review { get; set; }
    }    
}