using System;

namespace AwesomeFood.Contracts.Entities
{
    public interface IUserCreatedEntity : IEntity
    {
        Guid CreatedByUserId { get; set; }

        Guid ModifiedByUserId { get; set; }
    }
}