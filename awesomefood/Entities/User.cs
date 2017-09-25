using System;
using AwesomeFood.Contracts.Entities;

namespace AwesomeFood.Entities
{
    public class User : IUser
    {
        public Guid id { get; set; }
        public string Email { get; set; }
        public string DisplayName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime ModifiedOn { get; set; }
    }
}
