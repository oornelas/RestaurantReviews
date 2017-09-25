using System;

namespace AwesomeFood.Contracts.Entities
{
    public interface IEntity 
    {
        ///<summary>id is in lowercase because DocumentDB requires the name of the key to be id in lower case</summary>
        Guid id { get; set; }
        DateTime CreatedOn { get; set; }
        DateTime ModifiedOn { get; set; }
    }
}