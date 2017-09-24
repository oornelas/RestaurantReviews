using System;

namespace AwesomeFood.Contracts.Entities
{
    public interface IEntity 
    {
        Guid Id { get; set; }
        DateTime CreatedOn { get; set; }
        DateTime ModifiedOn { get; set; }
    }
}